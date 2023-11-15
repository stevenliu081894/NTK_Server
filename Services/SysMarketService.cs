using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.SysMarket;

namespace DB.Services
{
    public class SysMarketService
    {
        #region Dto

        public static SysMarketDto Find(string code)
        {
            string sql = @"SELECT * FROM `sys_market` WHERE `code` = @code";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {code});
                return conn.QueryFirstOrDefault<SysMarketDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysMarketService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<SysMarketDto> FindAll()
        {
            string sql = @"SELECT * FROM `sys_market`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<SysMarketDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysMarketService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(SysMarketDto model)
        {
			string sql = @"INSERT INTO `sys_market` (
				`code`, `currency`, `enable`, `name`, `sort`, `buy_fee`, `sell_fee`, `min_buy_fee`, `min_sell_fee`, `default_stock_code`)
				VALUES (@code, @currency, @enable, @name, @sort, @buy_fee, @sell_fee, @min_buy_fee, @min_sell_fee, @default_stock_code); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysMarketService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(SysMarketDto model)
        {
            string sql = @"UPDATE `sys_market` SET 
				`currency` = @currency,
				`enable` = @enable,
				`name` = @name,
				`sort` = @sort,
				`buy_fee` = @buy_fee,
				`sell_fee` = @sell_fee,
				`min_buy_fee` = @min_buy_fee,
				`min_sell_fee` = @min_sell_fee,
                `default_stock_code` = @default_stock_code
				 WHERE `code` = @code";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysMarketService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string code)
        {
            string sql = @"DELETE FROM `sys_market` WHERE `code` = @code";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { code });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysMarketService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<SysMarketList> FindSysMarketList()
        {
            string sql = @$"SELECT sys_market.code, sys_market.currency, sys_market.enable, sys_market.name, sys_market.sort, sys_market.buy_fee, sys_market.sell_fee, sys_market.min_buy_fee, sys_market.min_sell_fee FROM `sys_market`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<SysMarketList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysMarketService][FindSysMarketList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other
        public static SysMarketDto FindByCurrency(string currency)
        {
            string sql = @"SELECT * FROM `sys_market` WHERE `currency` = @currency";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { currency = currency.ToUpper() });
                return conn.QueryFirstOrDefault<SysMarketDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysMarketService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<SysMarketDto> FindDropDown(string lang)
        {
            string sql = @"SELECT `code`, `currency`, g.`value` AS `name` FROM sys_market
                INNER JOIN mutilang_table g ON g.`key` = sys_market.`code`
                WHERE sys_market.enable = 1 AND g.`dbtable` = ""sys_market"" AND g.`field` = ""name"" 
                AND g.`lang` = @lang ";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<SysMarketDto>(sql, new { lang = lang.ToUpper() }).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysMarketService][FindAll]" + ex.Message);
                return null;
            }
        }

        #endregion
    }
}
