using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.AdminLogin;

namespace DB.Services
{
    public class AdminLoginService
    {
        #region Dto

        public static AdminLoginDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_login` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminLoginDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLoginService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminLoginDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_login`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminLoginDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLoginService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminLoginDto source)
        {
            string sql = @"INSERT INTO `admin_login` (
				`ip`, `ip_country`, `login_account`, `device`, `create_time`, `status`, `remark`)
				VALUES (@ip, @ip_country, @login_account, @device, @create_time, @status, @remark);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLoginService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminLoginDto model)
        {
            string sql = @"UPDATE `admin_login` SET 
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
                LogLib.Log("[AdminLoginService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_login` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLoginService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<AdminLoginList> FindAdminLoginList(string whereSql="")
        {
            string sql = @$"SELECT admin_login.pk, admin_login.login_account, admin_login.create_time, admin_login.status, admin_login.ip, admin_login.ip_country, admin_login.remark, admin_login.device 
                FROM `admin_login`{whereSql} order by create_time DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminLoginList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminLoginService][FindAdminLoginList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
