using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class VerifyService
    {
        #region Dto

        public static VerifyDto Find(int pk)
        {
            string sql = @"SELECT * FROM `verify` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<VerifyDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[VerifyService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<VerifyDto> FindAll()
        {
            string sql = @"SELECT * FROM `verify`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<VerifyDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[VerifyService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(VerifyDto source)
        {
            string sql = @"INSERT INTO `verify` (
				`code`, `send_time`, `type`, `email`)
				VALUES (@code, @send_time, @type, @email);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[VerifyService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(VerifyDto model)
        {
            string sql = @"UPDATE `verify` SET 
				`code` = @code,
				`send_time` = @send_time,
				`type` = @type,
				`email` = @email
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[VerifyService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `verify` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[VerifyService][Remove]" + ex.Message);
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
