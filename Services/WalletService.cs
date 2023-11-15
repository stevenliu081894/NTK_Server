using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Wallet;

namespace DB.Services
{
    public class WalletService
    {
        #region Dto

        public static WalletDto Find(int member_fk)
        {
            string sql = @"SELECT * FROM `wallet` WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {member_fk});
                return conn.QueryFirstOrDefault<WalletDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(WalletDto model)
        {
			string sql = @"INSERT INTO `wallet` (
				`member_fk`, `currency`, `balance`, `freeze`, `margin`, `operate_balance`, `richbox_balance`, `status`, `coupon`, `total_recharge`, `total_withdraw`, `last_update_time`)
				VALUES (@member_fk, @currency, @balance, @freeze, @margin, @operate_balance, @richbox_balance, @status, @coupon, @total_recharge, @total_withdraw, @last_update_time); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(WalletDto model)
        {
            string sql = @"UPDATE `wallet` SET 
				`currency` = @currency,
				`balance` = @balance,
				`freeze` = @freeze,
				`margin` = @margin,
				`operate_balance` = @operate_balance,
				`richbox_balance` = @richbox_balance,
				`status` = @status,
				`coupon` = @coupon,
				`total_recharge` = @total_recharge,
				`total_withdraw` = @total_withdraw,
				`last_update_time` = @last_update_time
				 WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int member_fk)
        {
            string sql = @"DELETE FROM `wallet` WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { member_fk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 錢包 账户金额 balance 異動，change 正數 增加、負數 減少
        /// </summary>
        public static void BalanceMoneyChange(int member_fk, decimal change)
        {
            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    string sql = @"
                UPDATE wallet 
                SET balance = balance + @change
                WHERE member_fk = @member_fk";

                    var param = DapperMysql.GetParameters(new { change = change, member_fk = member_fk });
                    conn.Execute(sql, param);
                }
            }
            catch (Exception)
            {
                throw new AppException(1040, "write_db_exception");
            }
        }

        /// <summary>
        /// 錢包 冻结金额 freeze 異動，change 正數 增加、負數 減少
        /// </summary>
        public static void FreezeMoneyChange(int member_fk, decimal change)
        {
            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    string sql = @"
                UPDATE wallet 
                SET freeze = freeze + @change
                WHERE member_fk = @member_fk";

                    var param = DapperMysql.GetParameters(new { change = change, member_fk = member_fk });
                    conn.Execute(sql, param);
                }
            }
            catch (Exception)
            {
                throw new AppException(1040, "read_db_exception");
            }
        }

		/// <summary>
		/// 錢包 累计提现 total_withdraw 異動，change 正數 增加、負數 減少
		/// </summary>
		public static void TotalWithdrawChange(int member_fk, decimal change)
		{
			try
			{
				using (var conn = DapperMysql.GetWriteConntion())
				{
					string sql = @"UPDATE wallet 
                SET total_withdraw = total_withdraw + @total_withdraw
                WHERE member_fk = @member_fk";

					var param = DapperMysql.GetParameters(new { total_withdraw = change, member_fk = member_fk });
					conn.Execute(sql, param);
				}
			}
			catch (Exception)
			{
				throw new AppException(1040, "read_db_exception");
			}
		}

		/// <summary>
		/// 錢包 贈送金 coupon 異動，change 正數 增加、負數 減少
		/// </summary>
		public static void couponMoneyChange(int member_fk, decimal change)
        {
            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    string sql = @"
                UPDATE wallet 
                SET coupon = coupon + @change
                WHERE member_fk = @member_fk";

                    var param = DapperMysql.GetParameters(new { change = change, member_fk = member_fk });
                    conn.Execute(sql, param);
                }
            }
            catch (Exception)
            {
                throw new AppException(1040, "read_db_exception");
            }
        }

        /// <summary>
        /// 錢包 贈送金 gmoney 異動，change 正數 增加、負數 減少
        /// </summary>
        public static void GmoneyMoneyChange(int member_fk, decimal change)
        {
            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    string sql = @"
                UPDATE wallet 
                SET coupon = coupon + @change, last_update_time = UTC_TIMESTAMP()
                WHERE member_fk = @member_fk";

                    var param = DapperMysql.GetParameters(new { change = change, member_fk = member_fk });
                    conn.Execute(sql, param);
                }
            }
            catch (Exception)
            {
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 錢包 保证金总额 margin 異動，change 正數 增加、負數 減少
        /// </summary>
        public static void MarginMoneyChange(int member_fk, decimal change)
        {
            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    string sql = @"
                UPDATE wallet 
                SET margin = margin + @change
                WHERE member_fk = @member_fk";

                    var param = DapperMysql.GetParameters(new { change = change, member_fk = member_fk });
                    conn.Execute(sql, param);
                }
            }
            catch (Exception)
            {
                throw new AppException(1040, "read_db_exception");
            }
        }

        /// <summary>
        /// 錢包 操盘总额 operate_balance 異動，change 正數 增加、負數 減少
        /// </summary>
        public static void OperateMoneyChange(int member_fk, decimal change)
        {
            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    string sql = @"
                UPDATE wallet 
                SET operate_balance = operate_balance + @change
                WHERE member_fk = @member_fk";

                    var param = DapperMysql.GetParameters(new { change = change, member_fk = member_fk });
                    conn.Execute(sql, param);
                }
            }
            catch (Exception)
            {
                throw new AppException(1040, "read_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<WalletList> FindWalletList(string whereSql="")
        {
            string sql = @$"SELECT wallet.member_fk, member.account, member.nickname, wallet.currency, wallet.balance, wallet.freeze, wallet.status, wallet.coupon, wallet.total_recharge, wallet.total_withdraw, wallet.last_update_time, member.admin_user_fk, member.id_auth, member.last_login_time, member.last_login_ip 
                        FROM `wallet`
                        INNER JOIN member on member.pk = wallet.member_fk
                        {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletService][FindWalletList]" + ex.Message);
                return null;
            }
        }



        public static WalletEditVm FindWalletEditVm(int member_fk)
        {
            string sql = @$"SELECT wallet.member_fk, member.account, member.nickname, member.real_name, wallet.currency, wallet.balance, wallet.freeze, wallet.margin, wallet.richbox_balance, wallet.status, wallet.coupon, wallet.total_recharge, wallet.total_withdraw, wallet.last_update_time, member.admin_user_fk, member.email, member.id_auth, member.create_time, member.create_ip, member.last_login_time, member.last_login_ip, member.auth_time 
                            FROM `wallet`
                            INNER JOIN member on member.pk = wallet.member_fk
                            WHERE wallet.member_fk = {member_fk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<WalletEditVm>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletService][FindWalletEditVm]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
