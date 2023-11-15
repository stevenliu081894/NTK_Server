using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class HistoryDailyVnService
    {
        #region Dto

        public static HistoryDailyVnDto Find(int pk)
        {
            string sql = @"SELECT * FROM `history_daily_vn` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<HistoryDailyVnDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[HistoryDailyVnService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<HistoryDailyVnDto> FindAll()
        {
            string sql = @"SELECT * FROM `history_daily_vn`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<HistoryDailyVnDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[HistoryDailyVnService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(HistoryDailyVnDto source)
        {
            string sql = @"INSERT INTO `history_daily_vn` (
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
                LogLib.Log("[HistoryDailyVnService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(HistoryDailyVnDto model)
        {
            string sql = @"UPDATE `history_daily_vn` SET 
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
                LogLib.Log("[HistoryDailyVnService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `history_daily_vn` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[HistoryDailyVnService][Remove]" + ex.Message);
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
