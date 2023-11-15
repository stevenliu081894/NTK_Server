using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.StockVn;

namespace DB.Services
{
    public class StockVnService
    {
        #region Dto

        public static StockVnDto Find(string stock_code)
        {
            string sql = @"SELECT * FROM `stock_vn` WHERE `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {stock_code});
                return conn.QueryFirstOrDefault<StockVnDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockVnService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<StockVnDto> FindAll()
        {
            string sql = @"SELECT * FROM `stock_vn`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockVnDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockVnService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(StockVnDto model)
        {
			string sql = @"INSERT INTO `stock_vn` (
				`stock_code`, `stock_name`, `market`, `enable`, `disable_alwayse`, `program_enable`, `program_msg`, `main_switch`, `close_reason`, `opentrade`, `update_datetime`, `yclose`, `limitbuy`, `limitsell`, `final_price`, `volume`, `full_info`)
				VALUES (@stock_code, @stock_name, @market, @enable, @disable_alwayse, @program_enable, @program_msg, @main_switch, @close_reason, @opentrade, @update_datetime, @yclose, @limitbuy, @limitsell, @final_price, @volume, @full_info); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockVnService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static List<StockUsDto> FindEnableList()
        {
            using (var conn = DapperMysql.GetReadConnection())
            {
                string sql = @"
SELECT `stock_code`, `stock_name`, `market`, `enable`, `disable_alwayse`, `program_enable`, `main_switch`, `close_reason`, `opentrade`, `update_datetime`, `yclose`, `limitbuy`, `limitsell`, `final_price`, `volume`, `full_info`
FROM `stock_vn`
WHERE `main_switch` = 1
ORDER BY `stock_code`
";
                //var param = DapperMysql.GetParameters(new { stockcode = code });
                return conn.Query<StockUsDto>(sql).AsList();
            }
        }

        public static int UpdateFull(StockVnDto model)
        {
            model.main_switch = model.enable && !model.disable_alwayse && model.program_enable;

            string sql = @"UPDATE `stock_vn` SET 
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
                LogLib.Log("[StockVnService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string stock_code)
        {
            string sql = @"DELETE FROM `stock_vn` WHERE `stock_code` = @stock_code";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { stock_code });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockVnService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<StockVnList> FindStockVnList(string whereSql="")
        {
            string sql = @$"SELECT stock_vn.stock_code, stock_vn.stock_name, stock_vn.enable, stock_vn.disable_alwayse, stock_vn.program_enable, stock_vn.main_switch, stock_vn.program_msg, stock_vn.opentrade, stock_vn.update_datetime, stock_vn.yclose, stock_vn.final_price, stock_vn.volume, stock_vn.limitbuy, stock_vn.limitsell FROM `stock_vn`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockVnList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockVnService][FindStockVnList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
