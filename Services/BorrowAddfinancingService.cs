using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.BorrowAddfinancing;
using NTKServer.ViewModels.Addfinancing;
using NTKServer.ViewModels.HisAddfinancing;

namespace DB.Services
{
    public class BorrowAddfinancingService
    {
        #region Dto

        public static BorrowAddfinancingDto Find(int pk)
        {
            string sql = @"SELECT * FROM `borrow_addfinancing` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<BorrowAddfinancingDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<BorrowAddfinancingDto> FindAll()
        {
            string sql = @"SELECT * FROM `borrow_addfinancing`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowAddfinancingDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(BorrowAddfinancingDto source)
        {
            string sql = @"INSERT INTO `borrow_addfinancing` (
				`sub_account`, `borrow_fk`, `member_fk`, `currency`, `money`, `exchange`, `freeze`, `multiple`, `borrow_interest`, `last_deposit_money`, `last_borrow_money`, `status`, `add_time`, `verify_time`, `target_uid`, `target_name`, `coupon`)
				VALUES (@sub_account, @borrow_fk, @member_fk, @currency, @money, @exchange, @freeze, @multiple, @borrow_interest, @last_deposit_money, @last_borrow_money, @status, @add_time, @verify_time, @target_uid, @target_name, @coupon);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(BorrowAddfinancingDto model)
        {
            string sql = @"UPDATE `borrow_addfinancing` SET 
				`sub_account` = @sub_account,
				`borrow_fk` = @borrow_fk,
				`member_fk` = @member_fk,
				`currency` = @currency,
				`money` = @money,
				`exchange` = @exchange,
				`freeze` = @freeze,
				`multiple` = @multiple,
				`borrow_interest` = @borrow_interest,
				`last_deposit_money` = @last_deposit_money,
				`last_borrow_money` = @last_borrow_money,
				`status` = @status,
				`add_time` = @add_time,
				`verify_time` = @verify_time,
				`target_uid` = @target_uid,
				`target_name` = @target_name,
				`coupon` = @coupon
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `borrow_addfinancing` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<AddfinancingList> FindAddfinancingList(string whereSql="")
        {
            string sql = @$"SELECT borrow_addfinancing.pk, vw_trade_account.status AS trade_status, borrow_addfinancing.sub_account, vw_trade_account.loan_type, vw_trade_account.end_time, vw_trade_account.market, vw_trade_account.balance, vw_trade_account.warningline, borrow_addfinancing.money, borrow_addfinancing.borrow_interest, borrow_addfinancing.freeze, borrow_addfinancing.last_deposit_money, vw_trade_account.member_fk, vw_trade_account.account, vw_trade_account.member_name, borrow_addfinancing.add_time, borrow_addfinancing.status FROM `borrow_addfinancing`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AddfinancingList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][FindAddfinancingList]" + ex.Message);
                return null;
            }
        }


        public static AddfinancingReview FindAddfinancingReview(int pk)
        {
            string sql = @$"SELECT borrow_addfinancing.pk, vw_trade_account.status AS trade_status, borrow_addfinancing.sub_account, vw_trade_account.loan_type, vw_trade_account.end_time, vw_trade_account.market, vw_trade_account.balance, vw_trade_account.warningline, borrow_addfinancing.money, borrow_addfinancing.borrow_interest, borrow_addfinancing.freeze, borrow_addfinancing.last_deposit_money, vw_trade_account.member_fk, vw_trade_account.account, vw_trade_account.member_name, borrow_addfinancing.add_time, borrow_addfinancing.status FROM `borrow_addfinancing`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<AddfinancingReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][FindAddfinancingReview]" + ex.Message);
                return null;
            }
        }

        public static AddfinancingEditVm FindAddfinancingEditVm(int pk)
        {
            string sql = @$"SELECT borrow_addfinancing.pk, vw_trade_account.status AS trade_status, borrow_addfinancing.sub_account, vw_trade_account.loan_type, vw_trade_account.end_time, vw_trade_account.market, vw_trade_account.balance, vw_trade_account.warningline, borrow_addfinancing.money, borrow_addfinancing.borrow_interest, borrow_addfinancing.freeze, borrow_addfinancing.exchange, borrow_addfinancing.last_deposit_money, vw_trade_account.member_fk, vw_trade_account.account, vw_trade_account.member_name, borrow_addfinancing.add_time, borrow_addfinancing.status, borrow_addfinancing.verify_time, borrow_addfinancing.target_uid, borrow_addfinancing.target_name FROM `borrow_addfinancing`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<AddfinancingEditVm>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][FindAddfinancingEditVm]" + ex.Message);
                return null;
            }
        }

		public static List<HisAddfinancingList> FindHisAddfinancingList(string whereSql = "")
		{
			string sql = @$"SELECT borrow_addfinancing.status, vw_trade_account.account, vw_trade_account.member_name, 
                borrow_addfinancing.sub_account, vw_trade_account.market, vw_trade_account.loan_type, borrow_addfinancing.pk, 
                borrow_addfinancing.money, borrow_addfinancing.borrow_interest, borrow_addfinancing.exchange, borrow_addfinancing.freeze, 
                borrow_addfinancing.add_time, borrow_addfinancing.verify_time 
                FROM `borrow_addfinancing`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_addfinancing.sub_account
                {whereSql}";

			try
			{
				using var conn = DapperMysql.GetReadConnection();
				return conn.Query<HisAddfinancingList>(sql).AsList();
			}
			catch (Exception ex)
			{
				LogLib.Log("[BorrowAddfinancingService][FindHisAddfinancingList]" + ex.Message);
				return null;
			}
		}


		public static HisAddfinancingEditVm FindHisAddfinancingEditVm(int pk)
		{
			string sql = @$"SELECT borrow_addfinancing.status, vw_trade_account.account, vw_trade_account.member_name, borrow_addfinancing.sub_account, 
                vw_trade_account.market, vw_trade_account.loan_type, borrow_addfinancing.pk, borrow_addfinancing.money, borrow_addfinancing.borrow_interest, 
                borrow_addfinancing.exchange, borrow_addfinancing.freeze, borrow_addfinancing.add_time, borrow_addfinancing.verify_time 
                FROM `borrow_addfinancing`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_addfinancing.sub_account
                WHERE borrow_addfinancing.pk = {pk}";

			try
			{
				using var conn = DapperMysql.GetReadConnection();
				return conn.QueryFirstOrDefault<HisAddfinancingEditVm>(sql);
			}
			catch (Exception ex)
			{
				LogLib.Log("[BorrowAddfinancingService][FindHisAddfinancingEditVm]" + ex.Message);
				return null;
			}
		}

		#endregion

		#region Other

		public static List<BorrowAddfinancingSearchList> GetAddFinancing(string where = "")
        {
            string sql =
            $"SELECT a.pk AS pk, a.member_fk AS member_fk, m.real_name AS member_real_name, a.sub_account AS sub_account, a.multiple AS multiple, " +
            $"a.last_deposit_money AS last_deposit_money, a.last_borrow_money AS last_borrow_money, a.money AS money, a.currency AS currency, a.exchange AS EXCHANGE, " +
            $"a.freeze AS freeze, a.add_time AS add_time, a.borrow_interest AS borrow_interest, a.verify_time AS verify_time, a.target_name AS target_name, v.end_time AS end_time " +
            $"FROM borrow_addfinancing AS a " +
            $"LEFT JOIN member AS m ON m.pk = a.member_fk " +
            $"LEFT JOIN vw_trade_account AS v ON v.sub_account = a.sub_account {where} ;";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowAddfinancingSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][GetAddFinancing]" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 更新審核狀態
        /// </summary>
        public static int UpdateState(int id, bool state)
        {
            string sql = @"UPDATE `borrow_addfinancing` SET status = @status,verify_time=@verify_time WHERE pk=@pk;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    pk = id,
                    status = state ? (int)VerifyStatusType.Successful : (int)VerifyStatusType.Fail,
                    verify_time = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddfinancingService][UpdateState]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion
    }
}
