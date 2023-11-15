using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.TradeAccount;
using NTKServer.ViewModels.UserTradeDeal;

namespace DB.Services
{
    public class TradeDealService
    {
        #region Dto

        public static TradeDealDto Find(int pk)
        {
            string sql = @"SELECT * FROM `trade_deal` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<TradeDealDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeDealService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradeDealDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_deal`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeDealDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeDealService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(TradeDealDto source)
        {
            string sql = @"INSERT INTO `trade_deal` (
				`deal_id`, `sub_account`, `trade_order_sn`, `stock_code`, `stock_name`, `order_type`, `market`, `dir`, `final_price`, `final_volume`, `create_datetime`, `currency`, `total_pay`, `total_amount`, `total_cost`, `handling_fee`, `transfer_fee`, `stamp_fee`, `other_fee`)
				VALUES (@deal_id, @sub_account, @trade_order_sn, @stock_code, @stock_name, @order_type, @market, @dir, @final_price, @final_volume, @create_datetime, @currency, @total_pay, @total_amount, @total_cost, @handling_fee, @transfer_fee, @stamp_fee, @other_fee);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeDealService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(TradeDealDto model)
        {
            string sql = @"UPDATE `trade_deal` SET 
				`deal_id` = @deal_id,
				`sub_account` = @sub_account,
				`trade_order_sn` = @trade_order_sn,
				`stock_code` = @stock_code,
				`stock_name` = @stock_name,
				`order_type` = @order_type,
				`market` = @market,
				`dir` = @dir,
				`final_price` = @final_price,
				`final_volume` = @final_volume,
				`create_datetime` = @create_datetime,
				`currency` = @currency,
				`total_pay` = @total_pay,
				`total_amount` = @total_amount,
				`total_cost` = @total_cost,
				`handling_fee` = @handling_fee,
				`transfer_fee` = @transfer_fee,
				`stamp_fee` = @stamp_fee,
				`other_fee` = @other_fee
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeDealService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `trade_deal` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeDealService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<UserTradeDealList> FindUserTradeDealList(string whereSql="")
        {
            string sql = @$"SELECT trade_deal.pk, trade_deal.sub_account, trade_deal.deal_id, trade_deal.trade_order_sn, trade_deal.stock_code, 
                trade_deal.stock_name, trade_deal.order_type, trade_deal.dir, trade_deal.final_price, trade_deal.final_volume, 
                trade_deal.create_datetime, trade_deal.total_pay, trade_deal.total_cost 
                FROM `trade_deal` {whereSql} order by create_datetime DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<UserTradeDealList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeDealService][FindUserTradeDealList]" + ex.Message);
                return null;
            }
        }







        public static List<DealSearchList> FindTradeDealSearch(string where = "")
        {
            string sql = $"SELECT * FROM `trade_deal` {where}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<DealSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeDealService][FindTradeDealSearch]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
