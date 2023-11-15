using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class HistoryDailyUsService
    {
        #region Dto

        public static HistoryDailyUsDto Find(int pk)
        {
            string sql = @"SELECT * FROM `history_daily_us` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<HistoryDailyUsDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[HistoryDailyUsService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<HistoryDailyUsDto> FindAll()
        {
            string sql = @"SELECT * FROM `history_daily_us`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<HistoryDailyUsDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[HistoryDailyUsService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(HistoryDailyUsDto source)
        {
            string sql = @"INSERT INTO `history_daily_us` (
				`date`, `stock_code`, `open`, `high`, `low`, `close`, `volume`)
				VALUES (@date, @stock_code, @open, @high, @low, @close, @volume);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[HistoryDailyUsService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(HistoryDailyUsDto model)
        {
            string sql = @"UPDATE `history_daily_us` SET 
				`date` = @date,
				`stock_code` = @stock_code,
				`open` = @open,
				`high` = @high,
				`low` = @low,
				`close` = @close,
				`volume` = @volume
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[HistoryDailyUsService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `history_daily_us` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[HistoryDailyUsService][Remove]" + ex.Message);
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
