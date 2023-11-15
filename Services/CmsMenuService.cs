using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsMenuService
    {
        #region Dto

        public static CmsMenuDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_menu` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsMenuDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMenuService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsMenuDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_menu`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsMenuDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMenuService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsMenuDto source)
        {
            string sql = @"INSERT INTO `cms_menu` (
				`nid`, `pid`, `column`, `page`, `type`, `title`, `url`, `css`, `rel`, `target`, `sort`, `status`)
				VALUES (@nid, @pid, @column, @page, @type, @title, @url, @css, @rel, @target, @sort, @status);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMenuService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsMenuDto model)
        {
            string sql = @"UPDATE `cms_menu` SET 
				`nid` = @nid,
				`pid` = @pid,
				`column` = @column,
				`page` = @page,
				`type` = @type,
				`title` = @title,
				`url` = @url,
				`css` = @css,
				`rel` = @rel,
				`target` = @target,
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
                LogLib.Log("[CmsMenuService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_menu` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMenuService][Remove]" + ex.Message);
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
