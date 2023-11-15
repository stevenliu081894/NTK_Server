using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.TradeAccount;
using NTKServer.ViewModels.TradePosition;
using NTKServer.ViewModels.UserTradePosition;

namespace DB.Services
{
    public class TradePositionService
    {
        #region Dto

        public static TradePositionDto Find(string sub_account)
        {
            string sql = @$"SELECT * FROM `trade_position` WHERE `sub_account` = {sub_account}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<TradePositionDto>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradePositionDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_position`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradePositionDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(TradePositionDto model)
        {
            string sql = @"INSERT INTO `trade_position` (
				`sub_account`, `stock_code`, `stock_name`, `stock_type`, `market`, `holding_volume`, `stop_lose_pos`, `new_pos`, `close_pos`, `lastprice`, `total`, `cost_purchase`, `cost_volume`, `cost_price`, `live_volume`, `live_cost`)
				VALUES (@sub_account, @stock_code, @stock_name, @stock_type, @market, @holding_volume, @stop_lose_pos, @new_pos, @close_pos, @lastprice, @total, @cost_purchase, @cost_volume, @cost_price, @live_volume, @live_cost); ";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(TradePositionDto model)
        {
            string sql = @"UPDATE `trade_position` SET 
				`stock_name` = @stock_name,
				`stock_type` = @stock_type,
				`market` = @market,
				`holding_volume` = @holding_volume,
				`stop_lose_pos` = @stop_lose_pos,
				`new_pos` = @new_pos,
				`close_pos` = @close_pos,
				`lastprice` = @lastprice,
				`total` = @total,
				`cost_purchase` = @cost_purchase,
				`cost_volume` = @cost_volume,
				`cost_price` = @cost_price,
				`live_volume` = @live_volume,
				`live_cost` = @live_cost
				 WHERE `sub_account` = @sub_account AND `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string sub_account, string stock_code)
        {
            string sql = @"DELETE FROM `trade_position` WHERE `sub_account` = @sub_account AND `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { sub_account, stock_code });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

		public static TradePositionDto FindByCode(string sub_account, string stock_code)
		{
			string sql = @"SELECT * FROM `trade_position` WHERE `sub_account` = @sub_account AND `stock_code` = @stock_code";

			try
			{
				using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new { sub_account, stock_code });
				return conn.QueryFirstOrDefault<TradePositionDto>(sql, param);
			}
			catch (Exception ex)
			{
				LogLib.Log("[TradePositionService][Find]" + ex.Message);
				return null;
			}
		}
		#endregion

		#region ViewModel
	    public static List<UserTradePositionList> FindUserTradePositionList(string whereSql="")
        {
            string sql = @$"SELECT trade_position.sub_account, trade_position.stock_code, trade_position.stock_name, trade_position.stock_type, 
                    trade_position.holding_volume, trade_position.lastprice, trade_position.cost_price, trade_position.new_pos, 
                    trade_position.close_pos, trade_position.stop_lose_pos, trade_position.total,
                    (stock_type*(total-cost_purchase)) as profit
                    FROM `trade_position`
                    INNER JOIN vw_trade_account on vw_trade_account.sub_account = trade_position.sub_account
                    {whereSql} order by trade_position.stock_code";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<UserTradePositionList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][FindUserTradePositionList]" + ex.Message);
                return null;
            }
        }

		public static List<TradePositionList> FindTradePositionList(string whereSql="")
        {
            string sql = @$"SELECT trade_position.sub_account, trade_position.market, trade_position.stock_code, trade_position.stock_name, 
                    trade_position.stock_type, trade_position.holding_volume, trade_position.lastprice, trade_position.cost_price, 
                    trade_position.new_pos, trade_position.close_pos, trade_position.stop_lose_pos, trade_position.total, vw_trade_account.loan_type, 
                    vw_trade_account.end_time, vw_trade_account.member_fk, vw_trade_account.account, vw_trade_account.member_name,
                    (stock_type*(total-cost_purchase)) as profit
                    FROM `trade_position`
                    INNER JOIN vw_trade_account on vw_trade_account.sub_account = trade_position.sub_account
                    {whereSql} order by trade_position.stock_code";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradePositionList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][FindTradePositionList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// 更新持倉委賣凍結量
        /// </summary>
        public static int UpdateSellVolume(TradeOrderDto order)
        {
            const string sql = @"UPDATE trade_position
SET sell_volume = sell_volume - @free_volume
WHERE sub_account = @sub_account AND stock_code = @stock_code;";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { sub_account = order.sub_account, free_volume = order.free_volume, stock_code = order.stock_code });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][UpdateSellVolume]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }

        }

        public static int GetPositionBySubAccount(string sub_account)
        {
            var sql = "SELECT COUNT(*) FROM trade_position WHERE sub_account = @sub_account;";
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.ExecuteScalar<int>(sql, new { sub_account });
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradePositionService][GetPositionBySubAccount]" + ex.Message);
                return 0;
            }
        }

        #endregion
    }
}
