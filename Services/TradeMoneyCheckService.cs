using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Demo;
using NTKServer.ViewModels.TradeMoney;
using NTKServer.ViewModels.TradeMoneyCheck;
using NTKServer.ViewModels.HisTradeMoneyCheck;

namespace DB.Services
{
    public class TradeMoneyCheckService
    {
        #region Dto

        public static TradeMoneyCheckDto Find(int pk)
        {
            string sql = @"SELECT * FROM `trade_money_check` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<TradeMoneyCheckDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradeMoneyCheckDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_money_check`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeMoneyCheckDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(TradeMoneyCheckDto source)
        {
            string sql = @"INSERT INTO `trade_money_check` (
				`sub_account`, `sn`, `type`, `state`, `frozen`, `exchange`, `currency`, `amount`, `request_time`, `acccept_by`, `accept_time`)
				VALUES (@sub_account, @sn, @type, @state, @frozen, @exchange, @currency, @amount, @request_time, @acccept_by, @accept_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static VwTradeAccountDto GetVwTradeAccountBySubAccount(string sub_account)
        {
            string sql = $"SELECT * FROM `vw_trade_account` WHERE sub_account = {sub_account}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<VwTradeAccountDto>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][GetVwTradeAccountBySubAccount]" + ex.Message);
                return null;
            }
        }

        public static int UpdateFull(TradeMoneyCheckDto model)
        {
            string sql = @"UPDATE `trade_money_check` SET 
				`sub_account` = @sub_account,
				`sn` = @sn,
				`type` = @type,
				`state` = @state,
				`frozen` = @frozen,
				`exchange` = @exchange,
				`currency` = @currency,
				`amount` = @amount,
				`request_time` = @request_time,
				`acccept_by` = @acccept_by,
				`accept_time` = @accept_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `trade_money_check` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<HisTradeMoneyCheckList> FindHisTradeMoneyCheckList(string whereSql="")
        {
            string sql = @$"SELECT trade_money_check.request_time, trade_money_check.accept_time, vw_trade_account.account, 
                vw_trade_account.member_name, trade_money_check.sub_account, trade_money_check.currency, trade_money_check.frozen, 
                vw_trade_account.init_money, vw_trade_account.loan_type, trade_money_check.pk, trade_money_check.state, 
                trade_money_check.acccept_by 
                FROM `trade_money_check`
                INNER JOIN vw_trade_account ON vw_trade_account.sub_account = trade_money_check.sub_account
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<HisTradeMoneyCheckList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][FindHisTradeMoneyCheckList]" + ex.Message);
                return null;
            }
        }

        public static HisTradeMoneyCheckReview FindHisTradeMoneyCheckReview(int pk)
        {
            string sql = @$"SELECT trade_money_check.request_time, trade_money_check.accept_time, vw_trade_account.account, 
                vw_trade_account.member_name, trade_money_check.sn, trade_money_check.sub_account, trade_money_check.currency, 
                trade_money_check.frozen, vw_trade_account.init_money, vw_trade_account.balance, vw_trade_account.loan_type, 
                trade_money_check.state, trade_money_check.acccept_by 
                FROM `trade_money_check`
                INNER JOIN vw_trade_account ON vw_trade_account.sub_account = trade_money_check.sub_account
                WHERE trade_money_check.pk = {pk}";

			try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<HisTradeMoneyCheckReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][FindHisTradeMoneyCheckReview]" + ex.Message);
                return null;
            }
        }

	    public static List<TradeMoneyCheckList> FindTradeMoneyCheckList(string whereSql="")
        {
            string sql = @$"SELECT trade_money_check.pk, vw_trade_account.account, vw_trade_account.member_name, trade_money_check.sn, 
                trade_money_check.sub_account, trade_money_check.currency, trade_money_check.frozen, vw_trade_account.balance, 
                vw_trade_account.loan_type 
                FROM `trade_money_check`
                INNER JOIN vw_trade_account ON vw_trade_account.sub_account = trade_money_check.sub_account
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeMoneyCheckList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][FindTradeMoneyCheckList]" + ex.Message);
                return null;
            }
        }


        public static TradeMoneyCheckReview FindTradeMoneyCheckReview(int pk)
        {
            string sql = @$"SELECT vw_trade_account.account, vw_trade_account.member_name, trade_money_check.sn, trade_money_check.sub_account, 
                trade_money_check.currency, trade_money_check.frozen, vw_trade_account.balance, vw_trade_account.loan_type 
                FROM `trade_money_check`
                INNER JOIN vw_trade_account ON vw_trade_account.sub_account = trade_money_check.sub_account
                WHERE trade_money_check.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<TradeMoneyCheckReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][FindTradeMoneyCheckReview]" + ex.Message);
                return null;
            }
        }

        public static List<DemoSearchList> FindDemoSearch(string where = "")
        {
            string sql = $"SELECT pk, sn, sub_account, request_time FROM `trade_money_check` {where}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<DemoSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][FindAll]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// 取得申请提盈
        /// </summary>
        public static List<TradeMoneySearchList> GetTradeMoneyCheckApply(string where)
        {
            string sql = $"SELECT t.pk AS pk, m.pk AS member_fk, m.real_name AS member_real_name, t.sub_account AS sub_account, " +
                $"t.currency AS currency, t.frozen AS frozen, v.balance AS balance, v.warningline AS warningline, " +
                $"v.breakline AS breakline, t.exchange AS exchange,t.amount AS amount, t.request_time AS request_time, " +
                $"t.accept_time AS accept_time, t.acccept_by AS acccept_by, t.state AS state " +
                $"FROM trade_money_check AS t " +
                $"LEFT JOIN member AS m ON m.sub_account = t.sub_account " +
                $"LEFT JOIN vw_trade_account AS v ON v.sub_account = t.sub_account {where}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeMoneySearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][GetTradeMoneyCheckApply]" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 更新提盈審核
        /// </summary>
        public static int UpdateWithdraw(int pk, VerifyStatusType status)
        {
            string sql = @"UPDATE trade_money_check SET state = @state, accept_time = @accept_time WHERE pk=@pk;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    pk = pk,
                    state = (int)status,
                    accept_time = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][UpdateWithdraw]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion
    }
}
