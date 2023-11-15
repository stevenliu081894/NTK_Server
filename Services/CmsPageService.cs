using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsPageService
    {
        #region Dto

        public static CmsPageDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_page` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsPageDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPageService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsPageDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_page`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsPageDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPageService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsPageDto source)
        {
            string sql = @"INSERT INTO `cms_page` (
				`title`, `content`, `keywords`, `description`, `template`, `cover`, `view`, `status`)
				VALUES (@title, @content, @keywords, @description, @template, @cover, @view, @status);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPageService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsPageDto model)
        {
            string sql = @"UPDATE `cms_page` SET 
				`title` = @title,
				`content` = @content,
				`keywords` = @keywords,
				`description` = @description,
				`template` = @template,
				`cover` = @cover,
				`view` = @view,
				`status` = @status
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPageService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_page` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPageService][Remove]" + ex.Message);
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
