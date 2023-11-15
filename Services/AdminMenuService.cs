using Dapper;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;


namespace DB.Services
{
    public class AdminMenuService
    {
        #region Dto

        public static AdminMenuDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_menu` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminMenuDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminMenuService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminMenuDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_menu` order by sort";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminMenuDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminMenuService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminMenuDto source)
        {
            string sql = @"INSERT INTO `admin_menu` (
				`admin_module_fk`, `parent`, `module`, `title`, `title_key`, `icon`, `url_type`, `url_value`, `url_target`, `online_hide`, `sort`, `system_menu`, `status`, `parameter`)
				VALUES (@admin_module_fk, @parent, @module, @title, @title_key, @icon, @url_type, @url_value, @url_target, @online_hide, @sort, @system_menu, @status, @parameter);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminMenuService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminMenuDto model)
        {
            string sql = @"UPDATE `admin_menu` SET 
				`admin_module_fk` = @admin_module_fk,
				`parent` = @parent,
				`module` = @module,
				`title` = @title,
				`title_key` = @title_key,
				`icon` = @icon,
				`url_type` = @url_type,
				`url_value` = @url_value,
				`url_target` = @url_target,
				`online_hide` = @online_hide,
				`sort` = @sort,
				`system_menu` = @system_menu,
				`status` = @status,
				`parameter` = @parameter
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminMenuService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_menu` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminMenuService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static List<AdminMenuDto> FindAllByFirstMenu()
        {
            string sql = string.Format(@"SELECT * FROM `admin_menu` WHERE pk = parent");

            try
            {
                using (var conn = DapperMysql.GetReadConnection())
                {
                    return conn.Query<AdminMenuDto>(sql).AsList();  
                }
            }
            catch (Exception ex)
            {
                LogLib.Log(ex.Message);
                return null;
            }
        }

        #endregion

        #region ViewModel

        #endregion

        #region Other

        #endregion
    }
}
