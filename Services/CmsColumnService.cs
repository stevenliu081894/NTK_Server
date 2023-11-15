using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsColumnService
    {
        #region Dto

        public static CmsColumnDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_column` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsColumnDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsColumnService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsColumnDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_column`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsColumnDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsColumnService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsColumnDto source)
        {
            string sql = @"INSERT INTO `cms_column` (
				`pid`, `name`, `model`, `url`, `target`, `content`, `icon`, `index_template`, `list_template`, `detail_template`, `post_auth`, `sort`, `status`, `hide`, `rank_auth`, `type`)
				VALUES (@pid, @name, @model, @url, @target, @content, @icon, @index_template, @list_template, @detail_template, @post_auth, @sort, @status, @hide, @rank_auth, @type);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsColumnService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsColumnDto model)
        {
            string sql = @"UPDATE `cms_column` SET 
				`pid` = @pid,
				`name` = @name,
				`model` = @model,
				`url` = @url,
				`target` = @target,
				`content` = @content,
				`icon` = @icon,
				`index_template` = @index_template,
				`list_template` = @list_template,
				`detail_template` = @detail_template,
				`post_auth` = @post_auth,
				`sort` = @sort,
				`status` = @status,
				`hide` = @hide,
				`rank_auth` = @rank_auth,
				`type` = @type
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsColumnService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_column` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsColumnService][Remove]" + ex.Message);
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
