using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels;
using NTKServer.ViewModels.ReviewBorrow;
using NTKServer.ViewModels.HisBorrow;

namespace DB.Services
{
    public class BorrowService
    {
        #region Dto

        public static BorrowDto Find(int pk)
        {
            string sql = @"SELECT * FROM `borrow` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<BorrowDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<BorrowDto> FindAll()
        {
            string sql = @"SELECT * FROM `borrow`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(BorrowDto source)
        {
            string sql = @"INSERT INTO `borrow` (
				`sub_account`, `borrow_plan_fk`, `member_fk`, `agent_fk`, `order_id`, `status`, `market`, `borrow_type`, `currency`, `deposit_money`, `init_money`, `multiple`, `auto_renewal`, `borrow_money`, `borrow_interest`, `repayment_type`, `borrow_duration`, `position`, `rate`, `total`, `trading_time`, `loss_warn_sms_send`, `stock_money`, `total_coupon`, `total_fee`, `total_interest`, `create_time`, `begin_time`, `end_time`, `verify_time`)
				VALUES (@sub_account, @borrow_plan_fk, @member_fk, @agent_fk, @order_id, @status, @market, @borrow_type, @currency, @deposit_money, @init_money, @multiple, @auto_renewal, @borrow_money, @borrow_interest, @repayment_type, @borrow_duration, @position, @rate, @total, @trading_time, @loss_warn_sms_send, @stock_money, @total_coupon, @total_fee, @total_interest, @create_time, @begin_time, @end_time, @verify_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(BorrowDto model)
        {
            string sql = @"UPDATE `borrow` SET 
				`sub_account` = @sub_account,
				`borrow_plan_fk` = @borrow_plan_fk,
				`member_fk` = @member_fk,
				`agent_fk` = @agent_fk,
				`order_id` = @order_id,
				`status` = @status,
				`market` = @market,
				`borrow_type` = @borrow_type,
				`currency` = @currency,
				`deposit_money` = @deposit_money,
				`init_money` = @init_money,
				`multiple` = @multiple,
				`auto_renewal` = @auto_renewal,
				`borrow_money` = @borrow_money,
				`borrow_interest` = @borrow_interest,
				`repayment_type` = @repayment_type,
				`borrow_duration` = @borrow_duration,
				`position` = @position,
				`rate` = @rate,
				`total` = @total,
				`trading_time` = @trading_time,
				`loss_warn_sms_send` = @loss_warn_sms_send,
				`stock_money` = @stock_money,
				`total_coupon` = @total_coupon,
				`total_fee` = @total_fee,
				`total_interest` = @total_interest,
				`create_time` = @create_time,
				`begin_time` = @begin_time,
				`end_time` = @end_time,
				`verify_time` = @verify_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `borrow` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel


        public static HisBorrowEditVm FindHisBorrowEditVm(int pk)
        {
            string sql = @$"SELECT vw_trade_account.account, vw_trade_account.member_name, borrow.sub_account, borrow.status, borrow.market, 
            borrow.borrow_type, borrow.init_money, vw_trade_account.balance, borrow.deposit_money, borrow.multiple, borrow.auto_renewal, 
            borrow.pk, borrow.order_id, borrow.borrow_interest, borrow.begin_time, borrow.end_time, borrow.create_time, borrow.verify_time 
            FROM `borrow`
            INNER JOIN vw_trade_account ON vw_trade_account.sub_account = borrow.sub_account
            WHERE borrow.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<HisBorrowEditVm>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][FindHisBorrowEditVm]" + ex.Message);
                return null;
            }
        }



	    public static List<HisBorrowList> FindHisBorrowList(string whereSql="")
        {
            string sql = @$"SELECT * FROM
(SELECT vw_trade_account.account, vw_trade_account.member_name, borrow.sub_account, borrow.status, borrow.market, 
            borrow.borrow_type, borrow.init_money, vw_trade_account.balance, borrow.deposit_money, borrow.multiple, borrow.auto_renewal, 
            borrow.pk, borrow.order_id, borrow.borrow_interest, borrow.begin_time, borrow.end_time, borrow.create_time, borrow.verify_time 
            FROM `borrow`
            INNER JOIN vw_trade_account ON vw_trade_account.sub_account = borrow.sub_account
	    WHERE borrow.sub_account IS NOT NULL
UNION
SELECT     member.account, member.nickname AS member_name, borrow.sub_account, borrow.status, borrow.market, 
            borrow.borrow_type, borrow.init_money, 0 AS balance, borrow.deposit_money, borrow.multiple, borrow.auto_renewal, 
            borrow.pk, borrow.order_id, borrow.borrow_interest, borrow.begin_time, borrow.end_time, borrow.create_time, borrow.verify_time 
            FROM `borrow`
	    INNER JOIN `member` ON member.pk = borrow.member_fk
	    WHERE borrow.sub_account IS NULL
) AS t
{whereSql}
ORDER BY t.create_time";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<HisBorrowList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][FindHisBorrowList]" + ex.Message);
                return null;
            }
        }
		
        public static List<ReviewBorrowList> FindReviewBorrowList(string whereSql="")
        {
            string sql = @$"SELECT member_fk, member.account, member.nickname, borrow.pk, order_id, market, borrow_type, deposit_money, 
                borrow_money, borrow_interest, borrow.create_time, begin_time, end_time 
                FROM `borrow`
                INNER JOIN `member` ON member.pk = borrow.member_fk 
                {whereSql}  order by borrow.create_time DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<ReviewBorrowList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][FindReviewBorrowList]" + ex.Message);
                return null;
            }
        }


        #endregion

        #region Other

        /// <summary>
        /// 结算合约提前终止仓位
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subAccount"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static int InAdvanceReset(int id, string subAccount)
        {
            const string sql = @"UPDATE `borrow` SET stock_money = (select sum(total_amount) from trade_deal WHERE sub_account = @subAccount),
total_fee=(select sum(total_cost) from trade_deal WHERE sub_account = @subAccount),
total_coupon=(select sum(use_coupon) from  borrow_fee WHERE sub_account = @subAccount),
total_interest=(select sum(borrow_fee) from borrow_fee WHERE sub_account = @subAccount)
WHERE pk=@pk;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    pk = id,
                    subAccount
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][InAdvanceReset]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 取得新合約審核列表
        /// </summary>
        public static List<BorrowSearchList> GetBorrowApplyList(string where)
        {
            string sql = $"SELECT b.pk AS pk, b.member_fk AS member_fk, m.username AS member_username, m.real_name AS member_real_name, b.order_id AS order_id, b.`status` AS `status`, b.borrow_type AS borrow_type, b.market AS market, b.borrow_duration AS borrow_duration, b.auto_renewal AS auto_renewal, b.begin_time AS begin_time, b.end_time AS end_time, b.deposit_money AS deposit_money, b.borrow_money AS borrow_money, b.multiple AS multiple, b.rate AS rate, b.borrow_interest AS borrow_interest, b.init_money AS init_money, b.create_time AS create_time, b.verify_time AS verify_time " +
                $"FROM borrow AS b " +
                $"LEFT JOIN member AS m ON m.pk = b.member_fk {where};";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][GetStopTradingList]" + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// 取得Borrow
        /// </summary>
        public static BorrowApply GetBorrowApply(int id)
        {
            string sql =
               $"SELECT m.time_zone AS time_zone, p.warning_line AS warning_line, " +
               $"p.break_line AS break_line, b.pk AS pk, b.member_fk AS member_fk, b.borrow_plan_fk AS borrow_plan_fk, " +
               $"m.account AS member_username, m.real_name AS member_real_name, " +
               $"b.order_id AS order_id, b.`status` AS `status`, b.borrow_type AS borrow_type, b.currency AS currency, " +
               $"b.market AS market, b.borrow_duration AS borrow_duration, b.auto_renewal AS auto_renewal, " +
               $"b.begin_time AS begin_time, b.end_time AS end_time, b.deposit_money AS deposit_money, " +
               $"b.borrow_money AS borrow_money, b.multiple AS multiple, b.rate AS rate, b.borrow_interest AS borrow_interest, " +
               $"b.init_money AS init_money, b.total_coupon AS total_coupon, b.total_fee AS total_fee, b.create_time AS create_time, b.verify_time AS verify_time " +
               $"FROM borrow AS b " +
               $"LEFT JOIN member AS m ON m.pk = b.member_fk " +
               $"LEFT JOIN borrow_plan AS p ON p.pk = b.borrow_plan_fk " +
               $"WHERE b.pk = {id}";
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk = id });
                return conn.QueryFirstOrDefault<BorrowApply>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][GetBorrowApply]" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 更新審核狀態
        /// </summary>
        public static int UpdateStatus(int id, BorrowStatus state)
        {
            string sql = $"UPDATE `borrow` SET status = @status, verify_time = @verify_time WHERE pk=@pk;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    pk = id,
                    status =(int)state,
                    verify_time = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowService][UpdateStatus]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

		public static int UpdateSubAccount(int id, string subAccount)
		{
			string sql = $"UPDATE `borrow` SET sub_account = @subAccount WHERE pk=@pk;";
			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				return conn.Execute(sql, new
				{
					pk = id,
					subAccount
				});
			}
			catch (Exception ex)
			{
				LogLib.Log("[BorrowService][UpdateStatus]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}

		#endregion
	}
}
