using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.StockUs;
using NTKServer.Cache;
using Models.ViewModels;

namespace DB.Services
{
    public class StockUsService
    {
        #region Dto

        public static StockUsDto Find(string stock_code)
        {
            string sql = @"SELECT * FROM `stock_us` WHERE `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {stock_code});
                return conn.QueryFirstOrDefault<StockUsDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockUsService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<StockUsDto> FindAll()
        {
            string sql = @"SELECT * FROM `stock_us`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockUsDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockUsService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(StockUsDto model)
        {
			string sql = @"INSERT INTO `stock_us` (
				`stock_code`, `stock_name`, `market`, `enable`, `disable_alwayse`, `program_enable`, `program_msg`, `main_switch`, `close_reason`, `opentrade`, `update_datetime`, `yclose`, `limitbuy`, `limitsell`, `final_price`, `volume`, `full_info`)
				VALUES (@stock_code, @stock_name, @market, @enable, @disable_alwayse, @program_enable, @program_msg, @main_switch, @close_reason, @opentrade, @update_datetime, @yclose, @limitbuy, @limitsell, @final_price, @volume, @full_info); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockUsService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static List<StockUsDto> FindEnableList()
        {
            using (var conn = DapperMysql.GetReadConnection())
            {
                string sql = @"
SELECT `stock_code`, `stock_name`, `market`, `enable`, `disable_alwayse`, `program_enable`, `main_switch`, `close_reason`, `opentrade`, `update_datetime`, `yclose`, `limitbuy`, `limitsell`, `final_price`, `volume`, `full_info`
FROM `stock_us`
WHERE `main_switch` = 1
ORDER BY `stock_code`
";
                //var param = DapperMysql.GetParameters(new { stockcode = code });
                return conn.Query<StockUsDto>(sql).AsList();
            }
        }

        public static int UpdateFull(StockUsDto model)
        {
            model.main_switch = model.enable && !model.disable_alwayse && model.program_enable;

            string sql = @"UPDATE `stock_us` SET 
				`stock_name` = @stock_name,
				`market` = @market,
				`enable` = @enable,
				`disable_alwayse` = @disable_alwayse,
				`program_enable` = @program_enable,
				`program_msg` = @program_msg,
				`main_switch` = @main_switch,
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
                LogLib.Log("[StockUsService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string stock_code)
        {
            string sql = @"DELETE FROM `stock_us` WHERE `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { stock_code });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockUsService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<StockUsList> FindStockUsList(string whereSql="")
        {
            string sql = @$"SELECT stock_us.stock_code, stock_us.stock_name, stock_us.enable, stock_us.disable_alwayse, stock_us.program_enable, stock_us.main_switch, stock_us.program_msg, stock_us.opentrade, stock_us.update_datetime, stock_us.yclose, stock_us.final_price, stock_us.volume, stock_us.limitbuy, stock_us.limitsell FROM `stock_us`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockUsList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockUsService][FindStockUsList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
