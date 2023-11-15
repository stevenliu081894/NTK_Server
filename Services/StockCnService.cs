using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class StockCnService
    {
        #region Dto

        public static StockCnDto Find(string stock_code)
        {
            string sql = @"SELECT * FROM `stock_cn` WHERE `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {stock_code});
                return conn.QueryFirstOrDefault<StockCnDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockCnService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<StockCnDto> FindAll()
        {
            string sql = @"SELECT * FROM `stock_cn`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockCnDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockCnService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(StockCnDto model)
        {
			string sql = @"INSERT INTO `stock_cn` (
				`stock_code`, `stock_name`, `market`, `enable`, `disable_alwayse`, `close_reason`, `opentrade`, `update_datetime`, `yclose`, `limitbuy`, `limitsell`, `final_price`, `volume`, `full_info`)
				VALUES (@stock_code, @stock_name, @market, @enable, @disable_alwayse, @close_reason, @opentrade, @update_datetime, @yclose, @limitbuy, @limitsell, @final_price, @volume, @full_info); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockCnService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(StockCnDto model)
        {
            string sql = @"UPDATE `stock_cn` SET 
				`stock_name` = @stock_name,
				`market` = @market,
				`enable` = @enable,
				`disable_alwayse` = @disable_alwayse,
				`close_reason` = @close_reason,
				`opentrade` = @opentrade,
				`update_datetime` = @update_datetime,
				`yclose` = @yclose,
				`limitbuy` = @limitbuy,
				`limitsell` = @limitsell,
				`final_price` = @final_price,
				`volume` = @volume,
				`full_info` = @full_info
				 WHERE `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockCnService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string stock_code)
        {
            string sql = @"DELETE FROM `stock_cn` WHERE `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { stock_code });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockCnService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

        #endregion

        #region Other

        #endregion
    }
}
