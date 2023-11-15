using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.AdminLog;

namespace DB.Services
{
    public class AdminLogService
    {
        #region Dto

        public static AdminLogDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminLogDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLogService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminLogDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_log`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminLogDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLogService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminLogDto source)
        {
            string sql = @"INSERT INTO `admin_log` (
				`admin_action`, `admin_user`, `param`, `remark`, `create_time`)
				VALUES (@admin_action, @admin_user, @param, @remark, @create_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLogService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminLogDto model)
        {
            string sql = @"UPDATE `admin_log` SET 
				`admin_action` = @admin_action,
				`admin_user` = @admin_user,
				`table_name` = @table_name,
				`table_index` = @table_index,
				`action_ip` = @action_ip,
				`param` = @param,
				`remark` = @remark,
				`create_time` = @create_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLogService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLogService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<AdminLogList> FindAdminLogList(string whereSql="")
        {
            string sql = @$"SELECT admin_log.pk, admin_log.create_time, admin_user.account, admin_user.nickname, admin_log.remark, admin_log.action_ip 
                FROM `admin_log`
                INNER JOIN admin_user on admin_user.pk = admin_log.admin_user
                {whereSql} ORDER BY admin_log.create_time DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminLogList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLogService][FindAdminLogList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
