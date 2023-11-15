using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Login;
using NTKServer.ViewModels.AdminUser;

namespace DB.Services
{
    public class AdminUserService
    {
        #region Dto

        public static AdminUserDto FindByAccount(string account)
        {
            string sql = "SELECT * FROM `admin_user` where account = @account";

            try
            {
                using (var conn = DapperMysql.GetReadConnection())
                {                    
                    return conn.QueryFirstOrDefault<AdminUserDto>(sql, new { account });
                }
            }
            catch (Exception ex)
            {
                LogLib.Log(ex.Message);
                return null;
            }
        }
		
        public static AdminUserDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_user` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminUserDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminUserService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminUserDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_user` order by sort";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminUserDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminUserService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminUserDto source)
        {
            string sql = @"INSERT INTO `admin_user` (
				`account`, `role`, `password`, `status`, `nickname`, `is_admin`, `mobile`, `email`, `avatar`, `sort`, `lang`, `last_login_time`, `last_login_ip`, `change_password`)
				VALUES (@account, @role, @password, @status, @nickname, @is_admin, @mobile, @email, @avatar, @sort, @lang, @last_login_time, @last_login_ip, @change_password);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminUserService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminUserDto model)
        {
            string sql = @"UPDATE `admin_user` SET 
				`account` = @account,
				`role` = @role,
				`password` = @password,
				`status` = @status,
				`nickname` = @nickname,
				`is_admin` = @is_admin,
				`mobile` = @mobile,
				`email` = @email,
				`avatar` = @avatar,
				`sort` = @sort,
				`lang` = @lang,
				`last_login_time` = @last_login_time,
				`last_login_ip` = @last_login_ip,
				`change_password` = @change_password
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminUserService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_user` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminUserService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<AdminUserList> FindAdminUserList(string whereSql="")
        {
            string sql = @$"SELECT admin_user.pk, admin_user.account, admin_user.nickname, admin_role.name, 
                admin_user.status, admin_user.is_admin, admin_user.sort, admin_user.lang, admin_user.last_login_time 
                FROM `admin_user`
                INNER JOIN admin_role ON admin_role.pk = admin_user.role 
                {whereSql} order by admin_user.role, admin_user.account";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminUserList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminUserService][FindAdminUserList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        /// <summary>
        ///  查詢後台帳號
        /// </summary>
        public static List<AdminListVm> FindAdminList()
        {
            string sql = "SELECT account, nickname FROM `admin_user` where status = 1 order by sort";

            try
            {
                using (var conn = DapperMysql.GetReadConnection())
                {
                    return conn.Query<AdminListVm>(sql).AsList();
                }
            }
            catch (Exception ex)
            {
                LogLib.Log(ex.Message);
                return null;
            }
        }

        public static int UpdatePassword(string account, string newpassword, bool needChangePassword)
        {
            string sql = @"UPDATE `admin_user` SET
				`password` = @newpassword,
				`change_password` = @needChangePassword
				 WHERE `account` = @account ";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				var param = DapperMysql.GetParameters(new { newpassword, needChangePassword, account });
				return conn.Execute(sql, param);
			}
			catch (Exception ex)
			{
				LogLib.Log("[AdminUserService][UpdateFull]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}
		#endregion
	}
}
