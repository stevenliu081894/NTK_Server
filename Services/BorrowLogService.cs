using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class BorrowLogService
    {
        #region Dto

        public static BorrowLogDto Find(int pk)
        {
            string sql = @"SELECT * FROM `borrow_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<BorrowLogDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowLogService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<BorrowLogDto> FindAll()
        {
            string sql = @"SELECT * FROM `borrow_log`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowLogDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowLogService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(BorrowLogDto source)
        {
            string sql = @"INSERT INTO `borrow_log` (
				`borrow_fk`, `add_time`, `remark`, `logtype`)
				VALUES (@borrow_fk, @add_time, @remark, @logtype);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowLogService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(BorrowLogDto model)
        {
            string sql = @"UPDATE `borrow_log` SET 
				`borrow_fk` = @borrow_fk,
				`add_time` = @add_time,
				`remark` = @remark,
				`logtype` = @logtype
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowLogService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `borrow_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowLogService][Remove]" + ex.Message);
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
