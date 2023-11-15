using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class MoneyDailyExchangeService
    {
        #region Dto

        public static MoneyDailyExchangeDto Find(int pk)
        {
            string sql = @"SELECT * FROM `money_daily_exchange` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<MoneyDailyExchangeDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MoneyDailyExchangeService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MoneyDailyExchangeDto> FindAll()
        {
            string sql = @"SELECT * FROM `money_daily_exchange`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MoneyDailyExchangeDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MoneyDailyExchangeService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MoneyDailyExchangeDto source)
        {
            string sql = @"INSERT INTO `money_daily_exchange` (
				`date`, `currency_symbol`, `base_symbol`, `inward_rate`, `outward_rate`, `create_time`)
				VALUES (@date, @currency_symbol, @base_symbol, @inward_rate, @outward_rate, @create_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MoneyDailyExchangeService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(MoneyDailyExchangeDto model)
        {
            string sql = @"UPDATE `money_daily_exchange` SET 
				`date` = @date,
				`currency_symbol` = @currency_symbol,
				`base_symbol` = @base_symbol,
				`inward_rate` = @inward_rate,
				`outward_rate` = @outward_rate,
				`create_time` = @create_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MoneyDailyExchangeService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `money_daily_exchange` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MoneyDailyExchangeService][Remove]" + ex.Message);
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
