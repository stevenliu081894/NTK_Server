using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class MemberLoginLogService
    {
        #region Dto

        public static MemberLoginLogDto Find(int pk)
        {
            string sql = @"SELECT * FROM `member_login_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<MemberLoginLogDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLoginLogService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MemberLoginLogDto> FindAll()
        {
            string sql = @"SELECT * FROM `member_login_log`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberLoginLogDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLoginLogService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MemberLoginLogDto source)
        {
            string sql = @"INSERT INTO `member_login_log` (
				`member_fk`, `ip`, `ip_country`, `login_account`, `device`, `create_time`, `status`, `remark`)
				VALUES (@member_fk, @ip, @ip_country, @login_account, @device, @create_time, @status, @remark);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLoginLogService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(MemberLoginLogDto model)
        {
            string sql = @"UPDATE `member_login_log` SET 
				`member_fk` = @member_fk,
				`ip` = @ip,
				`ip_country` = @ip_country,
				`login_account` = @login_account,
				`device` = @device,
				`create_time` = @create_time,
				`status` = @status,
				`remark` = @remark
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLoginLogService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `member_login_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLoginLogService][Remove]" + ex.Message);
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
