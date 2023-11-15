using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Wallet;
using NTKServer.ViewModels.WalletRecharge;
using NTKServer.ViewModels.HisWalletRecharge;

namespace DB.Services
{
    public class WalletRechargeService
    {
        #region Dto

        public static WalletRechargeDto Find(int pk)
        {
            string sql = @"SELECT * FROM `wallet_recharge` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<WalletRechargeDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRechargeService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletRechargeDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet_recharge`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletRechargeDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRechargeService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(WalletRechargeDto source)
        {
            string sql = @"INSERT INTO `wallet_recharge` (
				`member_fk`, `agent_id`, `admin_bank_fk`, `order_no`, `money`, `type`, `fee`, `create_time`, `create_ip`, `line_bank`, `status`, `receipt_img`, `charge_type_id`, `form_name`, `currency`)
				VALUES (@member_fk, @agent_id, @admin_bank_fk, @order_no, @money, @type, @fee, @create_time, @create_ip, @line_bank, @status, @receipt_img, @charge_type_id, @form_name, @currency);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRechargeService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(WalletRechargeDto model)
        {
            string sql = @"UPDATE `wallet_recharge` SET 
				`member_fk` = @member_fk,
				`agent_id` = @agent_id,
				`admin_bank_fk` = @admin_bank_fk,
				`order_no` = @order_no,
				`money` = @money,
				`type` = @type,
				`fee` = @fee,
				`create_time` = @create_time,
				`create_ip` = @create_ip,
				`line_bank` = @line_bank,
				`status` = @status,
				`receipt_img` = @receipt_img,
				`charge_type_id` = @charge_type_id,
				`form_name` = @form_name,
				`currency` = @currency
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRechargeService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `wallet_recharge` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRechargeService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		#endregion

		#region ViewModel
	    public static List<HisWalletRechargeList> FindHisWalletRechargeList(string whereSql="")
        {
            string sql = @$"SELECT wallet_recharge.status, wallet_recharge.verify_admin_pk, admin_user.nickname AS admin_name, wallet_recharge.verify_time, wallet_recharge.reject_result, 
                    wallet_recharge.order_no, wallet_recharge.create_time, admin_bank.card, admin_bank.bank_name, admin_bank.payee, 
                    wallet_recharge.line_bank, wallet_recharge.pk, wallet_recharge.type, wallet_recharge.currency, wallet_recharge.money, 
                    wallet_recharge.exchange, wallet_recharge.wallet_amount, member.account, member.nickname 
                    FROM `wallet_recharge`
                    INNER JOIN `member` ON member.pk = wallet_recharge.member_fk
                    INNER JOIN admin_bank ON admin_bank.pk = wallet_recharge.admin_bank_fk
                    INNER JOIN admin_user ON admin_user.pk = wallet_recharge.verify_admin_pk
                    {whereSql} order by wallet_recharge.create_time DESC ";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<HisWalletRechargeList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRechargeService][FindHisWalletRechargeList]" + ex.Message);
                return null;
            }
        }

	    public static List<WalletRechargeList> FindWalletRechargeList(string whereSql="")
        {
            string sql = @$"SELECT wallet_recharge.create_time, admin_bank.card, admin_bank.bank_name, admin_bank.payee, wallet_recharge.order_no, 
                    wallet_recharge.line_bank, wallet_recharge.pk, wallet_recharge.type, wallet_recharge.currency, wallet_recharge.money, 
                    wallet_recharge.exchange, wallet_recharge.wallet_amount, wallet_recharge.member_fk, member.account, member.nickname, 
                    wallet_recharge.create_ip, member.admin_user_fk, member.id_auth, member.level_id 
                    FROM `wallet_recharge`
                    INNER JOIN `member` ON member.pk = wallet_recharge.member_fk
                    INNER JOIN admin_bank ON admin_bank.pk = wallet_recharge.admin_bank_fk
                    {whereSql} order by wallet_recharge.create_time DESC ";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletRechargeList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRechargeService][FindWalletRechargeList]" + ex.Message);
                return null;
            }
        }


        public static WalletRechargeReview FindWalletRechargeReview(int pk)
        {
            string sql = @$"SELECT wallet_recharge.create_time, admin_bank.card, admin_bank.bank_name, admin_bank.payee, wallet_recharge.order_no, 
                wallet_recharge.line_bank, wallet_recharge.pk, wallet_recharge.type, wallet_recharge.currency, wallet_recharge.money, 
                wallet_recharge.exchange, wallet_recharge.wallet_amount, wallet_recharge.member_fk, member.account, member.nickname, 
                wallet_recharge.create_ip, wallet_recharge.form_name, wallet_recharge.reject_result, member.admin_user_fk, member.id_auth, 
                member.level_id 
                FROM `wallet_recharge`
                INNER JOIN `member` ON member.pk = wallet_recharge.member_fk
                INNER JOIN admin_bank ON admin_bank.pk = wallet_recharge.admin_bank_fk
                WHERE wallet_recharge.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<WalletRechargeReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRechargeService][FindWalletRechargeReview]" + ex.Message);
                return null;
            }
        }

        public static List<WalletRechargeSearchList> GetRecharge(string where)
        {
			string sql = $"SELECT * FROM `wallet_recharge` {where}";
			try
			{
				using var conn = DapperMysql.GetReadConnection();
				return conn.Query<WalletRechargeSearchList>(sql).AsList();
			}
			catch (Exception ex)
			{
				LogLib.Log("[WalletRechargeService][GetRecharge]" + ex.Message);
				return null;
			}
		}

		#endregion

		#region Other

        /// <summary>
        /// 充值審核
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="adminPk"></param>
        /// <param name="status"></param>
        /// <param name="rejectResult"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
		public static int VerifyRecharge(int pk,int adminPk,int status,string? rejectResult)
		{
			string sql = @"UPDATE `wallet_recharge` SET 
				`verify_admin_pk` = @verifyAdminPk,
				`verify_time` = @verifyTime,
				`status` = @status,
				`reject_result` = @rejectResult				
				 WHERE `pk` = @pk";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    pk,
                    verifyAdminPk = adminPk,
                    verifyTime = DateTime.UtcNow,
                    status,
					rejectResult
				});
			}
			catch (Exception ex)
			{
				LogLib.Log("[WalletRechargeService][VerifyRecharge]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}

		#endregion
	}
}
