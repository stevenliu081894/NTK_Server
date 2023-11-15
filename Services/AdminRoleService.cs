using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.AdminRole;

namespace DB.Services
{
    public class AdminRoleService
    {
        #region Dto

        public static AdminRoleDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_role` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<AdminRoleDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminRoleService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminRoleDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_role`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminRoleDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminRoleService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminRoleDto source)
        {
            string sql = @"INSERT INTO `admin_role` (
				`admin_module_fk`, `name`, `description`, `model`, `admin_menu`, `sort`, `status`, `lock_delete`)
				VALUES (@admin_module_fk, @name, @description, @model, @admin_menu, @sort, @status, @lock_delete);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminRoleService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(AdminRoleDto model)
        {
            string sql = @"UPDATE `admin_role` SET 
                `admin_module_fk` = @admin_module_fk,
				`name` = @name,
				`description` = @description,
				`model` = @model,
				`admin_menu` = @admin_menu,
				`sort` = @sort,
				`status` = @status,
				`lock_delete` = @lock_delete
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminRoleService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_role` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminRoleService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
        public static List<AdminRoleList> FindAdminRoleList()
        {
            string sql = @"SELECT admin_module_fk, pk, name, description, status  FROM `admin_role` ORDER BY sort";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminRoleList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminRoleService][FindAdminRoleList]" + ex.Message);
                return null;
            }
        }

        internal static AdminRoleList FindAdminRole(int pk)
        {
            string sql = $"SELECT admin_module_fk, pk, name, description, status  FROM `admin_role` where ok = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<AdminRoleList>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminRoleService][FindAdminRoleList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
