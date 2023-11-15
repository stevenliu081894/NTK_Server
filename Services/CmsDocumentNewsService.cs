using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsDocumentNewsService
    {
        #region Dto

        public static CmsDocumentNewsDto Find(int aid)
        {
            string sql = @"SELECT * FROM `cms_document_news` WHERE `aid` = @aid";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {aid});
                return conn.QueryFirstOrDefault<CmsDocumentNewsDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentNewsService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsDocumentNewsDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_document_news`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsDocumentNewsDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentNewsService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(CmsDocumentNewsDto model)
        {
			string sql = @"INSERT INTO `cms_document_news` (
				`aid`, `content`, `summary`, `imgitem`)
				VALUES (@aid, @content, @summary, @imgitem); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentNewsService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(CmsDocumentNewsDto model)
        {
            string sql = @"UPDATE `cms_document_news` SET 
				`content` = @content,
				`summary` = @summary,
				`imgitem` = @imgitem
				 WHERE `aid` = @aid";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentNewsService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int aid)
        {
            string sql = @"DELETE FROM `cms_document_news` WHERE `aid` = @aid";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { aid });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentNewsService][Remove]" + ex.Message);
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
