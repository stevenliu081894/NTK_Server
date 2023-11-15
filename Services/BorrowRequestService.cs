using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.BorrowRequest;
using NTKServer.ViewModels.RequestRenew;
using NTKServer.ViewModels.RequestStop;
using NTKServer.ViewModels.HisRequestRenew;

namespace DB.Services
{
    public class BorrowRequestService
    {
        #region Dto

        public static BorrowRequestDto Find(int pk)
        {
            string sql = @"SELECT * FROM `borrow_request` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<BorrowRequestDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<BorrowRequestDto> FindAll()
        {
            string sql = @"SELECT * FROM `borrow_request`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowRequestDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(BorrowRequestDto source)
        {
            string sql = @"INSERT INTO `borrow_request` (
				`borrow_plan_fk`, `sub_account`, `borrow_fk`, `member_fk`, `type`, `borrow_fee`, `use_coupon`, `fee_received`, `borrow_duration`, `new_end_time`, `status`, `add_time`, `verify_time`)
				VALUES (@borrow_plan_fk, @sub_account, @borrow_fk, @member_fk, @type, @borrow_fee, @use_coupon, @fee_received, @borrow_duration, @new_end_time, @status, @add_time, @verify_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(BorrowRequestDto model)
        {
            string sql = @"UPDATE `borrow_request` SET 
				`borrow_plan_fk` = @borrow_plan_fk,
				`sub_account` = @sub_account,
				`borrow_fk` = @borrow_fk,
				`member_fk` = @member_fk,
				`type` = @type,
				`borrow_fee` = @borrow_fee,
				`use_coupon` = @use_coupon,
				`fee_received` = @fee_received,
				`borrow_duration` = @borrow_duration,
				`new_end_time` = @new_end_time,
				`status` = @status,
				`add_time` = @add_time,
				`verify_time` = @verify_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `borrow_request` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<HisRequestRenewList> FindHisRequestRenewList(string whereSql="")
        {
            string sql = @$"SELECT borrow_request.verify_time, borrow_request.status, borrow_request.borrow_fee, vw_trade_account.account, 
                vw_trade_account.member_name, borrow_request.add_time, borrow_request.sub_account, vw_trade_account.market, 
                vw_trade_account.loan_type, borrow_request.pk, borrow_request.borrow_duration, vw_trade_account.end_time, 
                borrow_request.new_end_time 
                FROM `borrow_request`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_request.sub_account
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<HisRequestRenewList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][FindHisRequestRenewList]" + ex.Message);
                return null;
            }
        }


        public static HisRequestRenewEditVm FindHisRequestRenewEditVm(int pk)
        {
            string sql = @$"SELECT borrow_request.verify_time, borrow_request.status, borrow_request.borrow_fee, vw_trade_account.account, 
                vw_trade_account.member_name, borrow_request.add_time, borrow_request.sub_account, vw_trade_account.market, 
                vw_trade_account.loan_type, borrow_request.member_fk, borrow_request.pk, borrow_request.borrow_duration, 
                vw_trade_account.end_time, borrow_request.new_end_time 
                FROM `borrow_request`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_request.sub_account
                WHERE borrow_request.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<HisRequestRenewEditVm>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][FindHisRequestRenewEditVm]" + ex.Message);
                return null;
            }
        }

	    public static List<RequestStopList> FindRequestStopList(string whereSql="")
        {
            string sql = @$"SELECT borrow_request.pk, borrow_request.add_time, borrow_request.sub_account, vw_trade_account.loan_type, 
                vw_trade_account.init_money, vw_trade_account.balance, vw_trade_account.end_time, borrow_request.member_fk, 
                vw_trade_account.account, vw_trade_account.member_name, vw_trade_account.warningline, vw_trade_account.breakline 
                FROM `borrow_request`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_request.sub_account
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RequestStopList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][FindRequestStopList]" + ex.Message);
                return null;
            }
        }


        public static RequestStopReview FindRequestStopReview(int pk)
        {
            string sql = @$"SELECT borrow_request.add_time, borrow_request.sub_account, vw_trade_account.loan_type, vw_trade_account.init_money, 
                vw_trade_account.balance, vw_trade_account.end_time, borrow_request.member_fk, vw_trade_account.account, vw_trade_account.member_name, 
                borrow_request.pk, vw_trade_account.warningline, vw_trade_account.breakline, borrow_request.status, borrow_request.verify_time 
                FROM `borrow_request`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_request.sub_account
                WHERE borrow_request.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<RequestStopReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][FindRequestStopReview]" + ex.Message);
                return null;
            }
        }

	    public static List<RequestRenewList> FindRequestRenewList(string whereSql="")
        {
            string sql = @$"SELECT borrow_request.add_time, borrow_request.sub_account, vw_trade_account.loan_type, borrow_request.member_fk, 
                vw_trade_account.account, vw_trade_account.member_name, borrow_request.pk, borrow_request.borrow_duration, vw_trade_account.end_time, 
                borrow_request.new_end_time, borrow_request.borrow_fee, vw_trade_account.market, vw_trade_account.margin, vw_trade_account.balance, 
                vw_trade_account.warningline, vw_trade_account.breakline 
                FROM `borrow_request`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_request.sub_account
                {whereSql} order by borrow_request.add_time DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RequestRenewList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][FindRequestRenewList]" + ex.Message);
                return null;
            }
        }


        public static RequestRenewReview FindRequestRenewReview(int pk)
        {
            string sql = @$"SELECT borrow_request.add_time, borrow_request.sub_account, vw_trade_account.loan_type, borrow_request.member_fk, 
                    vw_trade_account.account, vw_trade_account.member_name, borrow_request.pk, borrow_request.borrow_duration, 
                    vw_trade_account.end_time, borrow_request.new_end_time, borrow_request.borrow_fee, vw_trade_account.market, 
                    vw_trade_account.margin, vw_trade_account.balance, vw_trade_account.warningline, vw_trade_account.breakline, 
                    borrow_request.status, borrow_request.verify_time 
                    FROM `borrow_request` 
                    INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_request.sub_account
                    WHERE borrow_request.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<RequestRenewReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][FindRequestRenewReview]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<StopTradingSearchList> GetStopTradingList(string where = "")
        {
            string sql = $"SELECT  b.pk AS pk, b.member_fk AS member_fk, m.real_name AS member_real_name, b.sub_account AS sub_account, b.`status` AS status, " +
            $"t.end_time AS trade_account_end_time, b.add_time AS add_time, b.verify_time AS verify_time, p.name AS borrow_plane_name FROM borrow_request AS b " +
            $"LEFT JOIN `member` AS m ON b.member_fk = m.pk " +
            $"LEFT JOIN `trade_account` AS t ON m.pk = t.member_fk LEFT JOIN `borrow_plan` AS p ON b.borrow_plan_fk = p.pk {where} ";
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StopTradingSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][GetStopTradingList]" + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// 更新審核狀態
        /// </summary>
        public static int UpdateState(int id, bool state)
        {
            const string sql = @"UPDATE `borrow_request` SET status = @status,verify_time=@verify_time
                                WHERE pk=@pk;";

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
                LogLib.Log("[BorrowRequestService][UpdateState]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<StockRenewalSearchList> GetRenewal(string where = "")
        {
            string sql =
            $"SELECT " +
            $"b.member_fk AS member_fk, m.real_name AS member_real_name, b.sub_account AS sub_account, b.`status` AS status, t.end_time AS trade_account_end_time, b.add_time AS add_time, b.verify_time AS verify_time, p.name AS borrow_plan_name, b.borrow_fee AS borrow_fee, b.use_coupon AS use_coupon " +
            $"FROM `borrow_request` AS b " +
            $"LEFT JOIN `member` AS m ON b.member_fk = m.pk " +
            $"LEFT JOIN `trade_account` AS t ON m.pk = t.member_fk " +
            $"LEFT JOIN `borrow_plan` AS p ON p.pk = b.borrow_plan_fk {where};";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockRenewalSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowRequestService][GetRenewal]" + ex.Message);
                return null;
            }
        }

        #endregion
    }
}
