using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Promotion;

namespace DB.Services
{
    public class CmsPromotionService
    {
        #region Dto

        public static CmsPromotionDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_promotion` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsPromotionDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPromotionService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsPromotionDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_promotion`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsPromotionDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPromotionService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsPromotionDto source)
        {
            string sql = @"INSERT INTO `cms_promotion` (
				`lang`, `title`, `sort`, `on_active`, `view`, `trash`, `starttime`, `endtime`, `topic_content`, `img_url`, `outsite`, `url`, `summary`)
				VALUES (@lang, @title, @sort, @on_active, @view, @trash, @starttime, @endtime, @topic_content, @img_url, @outsite, @url, @summary);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPromotionService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsPromotionDto model)
        {
            string sql = @"UPDATE `cms_promotion` SET 
				`lang` = @lang,
				`title` = @title,
				`sort` = @sort,
				`on_active` = @on_active,
				`view` = @view,
				`trash` = @trash,
				`starttime` = @starttime,
				`endtime` = @endtime,
				`topic_content` = @topic_content,
				`img_url` = @img_url,
				`outsite` = @outsite,
				`url` = @url,
				`summary` = @summary
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPromotionService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_promotion` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPromotionService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<PromotionList> FindPromotionList(string whereSql="")
        {
            string sql = @$"SELECT cms_promotion.pk, cms_promotion.lang, cms_promotion.title, cms_promotion.view, cms_promotion.sort, cms_promotion.on_active, cms_promotion.trash, cms_promotion.starttime, cms_promotion.endtime FROM `cms_promotion` {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<PromotionList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPromotionService][FindPromotionList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
