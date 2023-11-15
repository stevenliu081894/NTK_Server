using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsModelService
    {
        #region Dto

        public static CmsModelDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_model` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsModelDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsModelService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsModelDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_model`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsModelDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsModelService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsModelDto source)
        {
            string sql = @"INSERT INTO `cms_model` (
				`name`, `title`, `table`, `type`, `icon`, `sort`, `system`, `status`)
				VALUES (@name, @title, @table, @type, @icon, @sort, @system, @status);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsModelService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsModelDto model)
        {
            string sql = @"UPDATE `cms_model` SET 
				`name` = @name,
				`title` = @title,
				`table` = @table,
				`type` = @type,
				`icon` = @icon,
				`sort` = @sort,
				`system` = @system,
				`status` = @status
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsModelService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_model` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsModelService][Remove]" + ex.Message);
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
