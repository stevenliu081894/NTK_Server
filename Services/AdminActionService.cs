using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class AdminActionService
    {
        #region Dto

        public static AdminActionDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_action` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminActionDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminActionService][Find]" + ex.Message);
                return null;
            }
        }

        public static AdminActionDto FindByAdminMenu(int admin_menu_fk, string lang)
        {
            try
            {
                using (var conn = DapperMysql.GetReadConnection())
                {
                    string strSql = @"SELECT * FROM admin_action WHERE admin_menu_fk = @Admin_menu_fk and lang = @Lang;";
                    var param = new { Admin_menu_fk = admin_menu_fk, Lang = lang };

                    return conn.QuerySingleOrDefault<AdminActionDto>(strSql, param);
                }
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminActionService][FindByAdminMenu]" + ex.Message);
                return null;
            }
        }

        public static List<AdminActionDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_action`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminActionDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminActionService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminActionDto source)
        {
            string sql = @"INSERT INTO `admin_action` (
				`admin_menu_fk`, `lang`, `title`, `module`, `method`, `remark`, `param`, `log`, `status`)
				VALUES (@admin_menu_fk, @lang, @title, @module, @method, @remark, @param, @log, @status);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminActionService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminActionDto model)
        {
            string sql = @"UPDATE `admin_action` SET 
				`admin_menu_fk` = @admin_menu_fk,
				`lang` = @lang,
				`title` = @title,
				`module` = @module,
				`method` = @method,
				`remark` = @remark,
				`param` = @param,
				`log` = @log,
				`status` = @status
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminActionService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_action` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminActionService][Remove]" + ex.Message);
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
