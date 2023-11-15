using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.EndTradeAccount;
using NTKServer.ViewModels.StatTradeAccount;

namespace DB.Services
{
    public class VwTradeAccountService
    {
        #region Dto

        public static VwTradeAccountDto Find(string sub_account)
        {
            string sql = @$"SELECT * FROM `vw_trade_account` where sub_account = '{sub_account}' ";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {});
                return conn.QueryFirstOrDefault<VwTradeAccountDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[VwTradeAccountService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<VwTradeAccountDto> FindAll()
        {
            string sql = @"SELECT * FROM `vw_trade_account`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<VwTradeAccountDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[VwTradeAccountService][FindAll]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region ViewModel
	    public static List<StatTradeAccountList> FindStatTradeAccountList(string whereSql="")
        {
            string sql = @$"SELECT vw_trade_account.market, vw_trade_account.loan_type, vw_trade_account.margin, vw_trade_account.loan_money, COUNT(*) AS dcount 
                        FROM `vw_trade_account`
                        {whereSql}
                        GROUP BY vw_trade_account.market, vw_trade_account.loan_type";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StatTradeAccountList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[VwTradeAccountService][FindStatTradeAccountList]" + ex.Message);
                return null;
            }
        }






	    public static List<EndTradeAccountList> FindEndTradeAccountList(string whereSql="")
        {
            string sql = @$"SELECT vw_trade_account.sub_account, vw_trade_account.loan_type, vw_trade_account.market, vw_trade_account.init_money, vw_trade_account.balance, 
                    vw_trade_account.margin, vw_trade_account.margin_float, vw_trade_account.begin_time, vw_trade_account.end_time, vw_trade_account.close_time, 
                    vw_trade_account.breakline, vw_trade_account.member_fk, vw_trade_account.account, vw_trade_account.member_name FROM `vw_trade_account` 
                    LEFT JOIN borrow_plan ON borrow_plan.market=vw_trade_account.market AND borrow_plan.name=vw_trade_account.loan_type
                    {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<EndTradeAccountList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[VwTradeAccountService][FindEndTradeAccountList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
