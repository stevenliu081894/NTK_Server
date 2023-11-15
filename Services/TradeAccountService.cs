using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using Microsoft.AspNetCore.Connections;
using NTKServer.ViewModels.TradeAccount;
using NTKServer.ViewModels.TradingAccount;

namespace DB.Services
{
    public class TradeAccountService
    {
        #region Dto

        public static TradeAccountDto Find(string sub_account)
        {
            string sql = @"SELECT * FROM `trade_account` WHERE `sub_account` = @sub_account";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { sub_account });
                return conn.QueryFirstOrDefault<TradeAccountDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradeAccountDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_account`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeAccountDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(TradeAccountDto model)
        {
            string sql = @"INSERT INTO `trade_account` (
				`sub_account`, `member_fk`, `borrow_plan_fk`, `type`, `market`, `loan_type`, `currency`, `mem_money`, `frozen_money`, `margin`, `margin_float`, `loan_money`, `time_zone`, `begin_time`, `end_time`, `close_time`, `status`, `warningline`, `breakline`, `notice_warning`, `notice_close`, `live_state`, `live_balance`, `live_breakline`, `live_Broker`, `live_account_fk`, `multiple`, `borrow_duration`)
				VALUES (@sub_account, @member_fk, @borrow_plan_fk, @type, @market, @loan_type, @currency, @mem_money, @frozen_money, @margin, @margin_float, @loan_money, @time_zone, @begin_time, @end_time, @close_time, @status, @warningline, @breakline, @notice_warning, @notice_close, @live_state, @live_balance, @live_breakline, @live_Broker, @live_account_fk, @multiple, @borrow_duration); ";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(TradeAccountDto model)
        {
            string sql = @"UPDATE `trade_account` SET 
				`member_fk` = @member_fk,
				`borrow_plan_fk` = @borrow_plan_fk,
				`type` = @type,
				`market` = @market,
				`loan_type` = @loan_type,
				`currency` = @currency,
				`mem_money` = @mem_money,
				`frozen_money` = @frozen_money,
				`margin` = @margin,
				`margin_float` = @margin_float,
				`loan_money` = @loan_money,
				`time_zone` = @time_zone,
				`begin_time` = @begin_time,
				`end_time` = @end_time,
				`close_time` = @close_time,
				`status` = @status,
				`warningline` = @warningline,
				`breakline` = @breakline,
				`notice_warning` = @notice_warning,
				`notice_close` = @notice_close,
				`live_state` = @live_state,
				`live_balance` = @live_balance,
				`live_breakline` = @live_breakline,
				`live_Broker` = @live_Broker,
				`live_account_fk` = @live_account_fk
				 WHERE `sub_account` = @sub_account";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string sub_account)
        {
            string sql = @"DELETE FROM `trade_account` WHERE `sub_account` = @sub_account";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { sub_account });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
		
        public static List<TradingAccountList> FindTradingAccountList(string whereSql="")
        {
            string sql = @$"SELECT vw_trade_account.market, sub_account, balance, status, loan_type, warningline, breakline, position_value, 
                begin_time, end_time, member_fk, account, member_name FROM `vw_trade_account` 
                LEFT JOIN borrow_plan ON borrow_plan.market=vw_trade_account.market AND borrow_plan.name=vw_trade_account.loan_type
                {whereSql}
                ORDER BY `status` DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradingAccountList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][FindTradingAccountList]" + ex.Message);
                return null;
            }
        }


        public static List<TradeAccountSearchList> FindTradeAccountSearch(string where = "")
        {
            const string sql = @"SELECT 
a.sub_account AS sub_account, a.`status` AS status, a.`type` AS type, 
m.account AS member_name, m.nickname AS member_username, m.mobile AS member_mobile, 
a.loan_type AS loan_type, a.market AS market, a.currency AS currency, 
a.begin_time as begin_time, a.end_time AS end_time, a.loan_money AS loan_money, 
a.margin AS margin, a.mem_money AS mem_money, a.frozen_money AS frozen_money, 
a.warningline AS warningline , a.breakline AS breakline, SUM(p.total) AS total 
FROM `trade_account` AS a 
LEFT JOIN `member` AS m ON a.member_fk = m.pk 
LEFT JOIN `trade_position` AS p ON a.sub_account = p.sub_account GROUP BY a.sub_account;";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeAccountSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][FindTradeAccountSearch]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// 提前結束交易
        /// </summary>
        /// <param name="subAccount"></param>
        /// <param name="closeTime"></param>
        /// <exception cref="AppException"></exception>
        public static int InAdvanceClose(string subAccount, DateTime closeTime)
        {
            const string sql = @$"UPDATE trade_account SET status = @status, close_time=@close_time" +
                "WHERE sub_account = @subAccount;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    subAccount = subAccount,
                    status = (int)AccountStatusType.End,
                    close_time = closeTime
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][InAdvanceClose]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }

        }

        /// <summary>
        /// 更新帳戶續期狀態
        /// </summary>
        public static int UpdateAccountRenewal(string subAccount, AccountStatusType status, DateTime endDate)
        {
            const string sql = @"UPDATE trade_account SET status = @status ,end_time=@endDate,close_time=null
WHERE sub_account=@sub_account;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    sub_account = subAccount,
                    status = (int)status,
                    endDate = endDate,
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][UpdateAccountRenewal]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 修改擴大融資
        /// </summary>
        /// <param name="subAccount">子帳號</param>
        /// <param name="effectMoney">扩大配资金额</param>
        /// <param name="multiple">融资倍数</param>
        /// <param name="warninglineMultiple">預警線設置</param>
        /// <param name="breaklineMultiple">平倉線設置</param>
        /// <exception cref="AppException"></exception>
        public static int UpdateExpandBorrow(string subAccount, decimal effectMoney, decimal multiple, decimal warninglineMultiple, decimal breaklineMultiple)
        {
            const string sql = @$"UPDATE trade_account SET mem_money = mem_money + @effectMoney + @finance, margin = margin + @effectMoney, margin_float = margin_float + @effectMoney, " +
                "loan_money = loan_money + @finance, " +
                "warningline = (loan_money + @finance) + ((margin + @effectMoney) * @warninglineMultiple), " + // 融資金額 + (初始保證金 * (1-預警線設置))
                "breakline = (loan_money + @finance) + ((margin + @effectMoney) * @breaklineMultiple) " +   // 融資金額 + (初始保證金 * (1-平倉線設置))
                "WHERE sub_account = @subAccount;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    subAccount,
                    effectMoney,
                    finance = effectMoney * multiple,
                    warninglineMultiple,
                    breaklineMultiple
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][UpdateExpandBorrow]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 變更交易帳戶金額
        /// </summary>
        public static int UpdateMoney(string subAccount, decimal money)
        {
            const string sql = @"UPDATE trade_account SET mem_money = mem_money + @money
WHERE sub_account = @sub_account;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    sub_account = subAccount,
                    money = Convert.ToInt32(money),
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][UpdateMoney]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 更新账户提盈
        /// </summary>
        public static int UpdateTradeAccountVolume(bool verifyStatus, string subAccount, decimal frozen)
        {
            string sql;
            if (verifyStatus)
            {
                sql = $"UPDATE trade_account AS t " +
                    $"SET t.mem_money = t.mem_money - @frozen, frozen_money = t.frozen_money - @frozen " +
                    $"WHERE t.sub_account = @sub_account ";
            }
            else
            {
                sql = $"UPDATE trade_account AS t " +
                    $"SET t.mem_money = frozen_money = t.frozen_money - @frozen " +
                    $"WHERE t.sub_account = @sub_account ";
            }

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    sub_account = subAccount,
                    frozen = frozen
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][UpdateTradeAccountVolume]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 更新帳戶凍結金額
        /// </summary>
        public static int UpdateAccountFrozenMoney(TradeOrderDto order)
        {
            const string sql = @"UPDATE trade_account
SET frozen_money = frozen_money - @free_volume * @price
WHERE sub_account=@sub_account;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { sub_account = order.sub_account, free_volume = order.free_volume, price = order.price });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeAccountService][UpdateAccountFrozenMoney]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion
    }
}
