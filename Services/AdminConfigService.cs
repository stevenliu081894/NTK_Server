using Dapper;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.AdminConfig;

namespace DB.Services
{
    public class AdminConfigService
    {
        #region Dto

        public static AdminConfigDto Find(string name)
        {
            string sql = @"SELECT * FROM `admin_config` WHERE `name` = @name";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {name});
                return conn.QueryFirstOrDefault<AdminConfigDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminConfigService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminConfigDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_config`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminConfigDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminConfigService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(AdminConfigDto model)
        {
			string sql = @"INSERT INTO `admin_config` (
				`name`, `title`, `group`, `type`, `value`, `options`, `tips`, `ajax_url`, `next_items`, `param`, `format`, `table`, `level`, `key`, `option`, `pid`, `ak`, `sort`, `status`)
				VALUES (@name, @title, @group, @type, @value, @options, @tips, @ajax_url, @next_items, @param, @format, @table, @level, @key, @option, @pid, @ak, @sort, @status); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminConfigService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        
        public static int InsertConfig(AdminConfigVm model)
        {
			string sql = @"INSERT INTO `admin_config` (
				`name`, `title`, `group`, `type`, `value`, `options`, `tips`, `level`, `sort`, `status`)
				VALUES (@name, @title, @group, @type, @value, @options, @tips, @level, @sort, @status); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminConfigService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(AdminConfigDto model)
        {
            string sql = @"UPDATE `admin_config` SET 
				`title` = @title,
				`group` = @group,
				`type` = @type,
				`value` = @value,
				`options` = @options,
				`tips` = @tips,
				`ajax_url` = @ajax_url,
				`next_items` = @next_items,
				`param` = @param,
				`format` = @format,
				`table` = @table,
				`level` = @level,
				`key` = @key,
				`option` = @option,
				`pid` = @pid,
				`ak` = @ak,
				`sort` = @sort,
				`status` = @status
				 WHERE `name` = @name";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminConfigService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

		public static int UpdateConfig(AdminConfigDto model)
		{
			string sql = @"UPDATE `admin_config` SET 
				`title` = @title,
				`group` = @group,
				`value` = @value,
				`tips` = @tips,
				`format` = @format,
				`sort` = @sort,
				`status` = @status
				 WHERE `name` = @name";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				return conn.Execute(sql, model);
			}
			catch (Exception ex)
			{
				LogLib.Log("[AdminConfigService][UpdateFull]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}

		public static int Remove(string name)
        {
            string sql = @"DELETE FROM `admin_config` WHERE `name` = @name";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { name });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminConfigService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<AdminConfigList> FindAdminConfigList(string whereSql="")
        {
            string sql = @$"SELECT admin_config.name, admin_config.title, admin_config.group, admin_config.value, admin_config.status FROM `admin_config`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminConfigList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminConfigService][FindAdminConfigList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
