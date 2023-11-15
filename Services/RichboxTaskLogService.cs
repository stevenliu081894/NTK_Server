using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class RichboxTaskLogService
    {
        #region Dto

        public static RichboxTaskLogDto Find(int pk)
        {
            string sql = @"SELECT * FROM `richbox_task_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<RichboxTaskLogDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxTaskLogService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RichboxTaskLogDto> FindAll()
        {
            string sql = @"SELECT * FROM `richbox_task_log`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RichboxTaskLogDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxTaskLogService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(RichboxTaskLogDto source)
        {
            string sql = @"INSERT INTO `richbox_task_log` (
				`member_fk`, `code`, `info`, `create_time`, `result`)
				VALUES (@member_fk, @code, @info, @create_time, @result);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxTaskLogService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(RichboxTaskLogDto model)
        {
            string sql = @"UPDATE `richbox_task_log` SET 
				`member_fk` = @member_fk,
				`code` = @code,
				`info` = @info,
				`create_time` = @create_time,
				`result` = @result
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxTaskLogService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `richbox_task_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxTaskLogService][Remove]" + ex.Message);
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
