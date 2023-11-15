using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.QuestionCategory;

namespace DB.Services
{
    public class CmsQuestionCategoryService
    {
        #region Dto
        public static List<CmsQuestionCategoryDto> FindDropDown(string lang)
        {
			string sql = @"SELECT * FROM `cms_question_category` ";

			if (!string.IsNullOrWhiteSpace(lang))
            {
                sql = $"{sql} WHERE `lang` = @lang";
            }

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsQuestionCategoryDto>(sql, new { lang = lang.ToUpper() }).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionService][FindDropDown]" + ex.Message);
                return null;
            }
        }

        public static CmsQuestionCategoryDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_question_category` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsQuestionCategoryDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionCategoryService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsQuestionCategoryDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_question_category`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsQuestionCategoryDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionCategoryService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(CmsQuestionCategoryDto model)
        {
			string sql = @"INSERT INTO `cms_question_category` (
				`enable`, `label`, `icon`, `sort`, `lang`)
				VALUES (@enable, @label, @icon, @sort, @lang); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionCategoryService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(CmsQuestionCategoryDto model)
        {
            string sql = @"UPDATE `cms_question_category` SET 
				`enable` = @enable,
				`label` = @label,
				`icon` = @icon,
				`sort` = @sort,
				`lang` = @lang
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionCategoryService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_question_category` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionCategoryService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<QuestionCategoryList> FindQuestionCategoryList(string whereSql="")
        {
            string sql = @$"SELECT cms_question_category.pk, cms_question_category.enable, cms_question_category.label, 
                cms_question_category.icon, cms_question_category.sort, cms_question_category.lang
                FROM `cms_question_category`
                {whereSql} order by sort";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<QuestionCategoryList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionCategoryService][FindQuestionCategoryList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
