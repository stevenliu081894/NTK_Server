using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Document;

namespace DB.Services
{
    public class CmsDocumentService
    {
        #region Dto

        public static CmsDocumentDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_document` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsDocumentDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentService][Find]" + ex.Message);
                return null;
            }
        }

        public static CmsDocumentDto FindByCIDLANG(string cid, string lang)
        {
            string sql = @"SELECT * FROM `cms_document` WHERE `cid` = @cid and `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { cid, lang });
                return conn.QueryFirstOrDefault<CmsDocumentDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentService][FindByCIDLANG]" + ex.Message);
                return null;
            }
        }

        public static List<CmsDocumentDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_document`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsDocumentDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsDocumentDto source)
        {
            string sql = @"INSERT INTO `cms_document` (
				`cid`, `lang`, `title`, `content`, `flag`, `view`, `comment`, `good`, `bad`, `mark`, `sort`, `status`, `trash`)
				VALUES (@cid, @lang, @title, @content, @flag, @view, @comment, @good, @bad, @mark, @sort, @status, @trash);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsDocumentDto model)
        {
            string sql = @"UPDATE `cms_document` SET 
				`cid` = @cid,
				`lang` = @lang,
				`title` = @title,
				`content` = @content,
				`flag` = @flag,
				`view` = @view,
				`comment` = @comment,
				`good` = @good,
				`bad` = @bad,
				`mark` = @mark,
				`sort` = @sort,
				`status` = @status,
				`trash` = @trash
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_document` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<DocumentList> FindDocumentList(string whereSql="")
        {
            string sql = @$"SELECT cms_document.pk, cms_document.cid, cms_document.lang, cms_document.title, cms_document.view, cms_document.status FROM `cms_document`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<DocumentList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsDocumentService][FindDocumentList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
