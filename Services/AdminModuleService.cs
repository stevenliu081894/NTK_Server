using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class AdminModuleService
    {
        #region Dto

        public static AdminModuleDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_module` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminModuleDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminModuleService][Find]" + ex.Message);
                return null;
            }
        }

        public static AdminModuleDto? FindByAdminUser(int admin_user_pk)
        {
            string sql = @"
SELECT T1.* 
FROM `admin_module` T1
INNER JOIN `admin_role` T2 ON (T1.pk = T2.admin_module_fk)
INNER JOIN `admin_user` T3 ON (T2.pk = T3.role)
WHERE T3.`pk` = @admin_user_pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { admin_user_pk });
                return conn.QueryFirstOrDefault<AdminModuleDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminModuleService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminModuleDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_module` order by sort";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminModuleDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminModuleService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static List<AdminModuleDto> FindAll(int status)
        {
            string sql = @"
SELECT * FROM `admin_module` 
WHERE status = @status
order by sort";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = new { status };
                return conn.Query<AdminModuleDto>(sql, param).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminModuleService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminModuleDto source)
        {
            string sql = @"INSERT INTO `admin_module` (
				`name`, `title`, `icon`, `description`, `identifier`, `system_module`, `sort`, `status`)
				VALUES (@name, @title, @icon, @description, @identifier, @system_module, @sort, @status);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminModuleService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminModuleDto model)
        {
            string sql = @"UPDATE `admin_module` SET 
				`name` = @name,
				`title` = @title,
				`icon` = @icon,
				`description` = @description,
				`identifier` = @identifier,
				`system_module` = @system_module,
				`sort` = @sort,
				`status` = @status
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminModuleService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_module` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminModuleService][Remove]" + ex.Message);
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
