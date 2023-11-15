using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Question;

namespace DB.Services
{
    public class CmsQuestionService
    {
        #region Dto

        public static CmsQuestionDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_question` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsQuestionDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsQuestionDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_question`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsQuestionDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsQuestionDto source)
        {
            string sql = @"INSERT INTO `cms_question` (
				`question_category_fk`, `lang`, `question`, `answer`, `enable`, `commonly_used`, `sort`)
				VALUES (@question_category_fk, @lang, @question, @answer, @enable, @commonly_used, @sort);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsQuestionDto model)
        {
            string sql = @"UPDATE `cms_question` SET 
				`question_category_fk` = @question_category_fk,
                `lang` = @lang,
				`question` = @question,
				`answer` = @answer,
				`enable` = @enable,
				`commonly_used` = @commonly_used,
				`sort` = @sort
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_question` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<QuestionList> FindQuestionList(string whereSql="")
        {
            string sql = @$"SELECT cq.question_category_fk, cq.pk, cq.lang, cq.question, cq.enable, cq.commonly_used, cq.sort, cqc.label AS question_category_text FROM `cms_question` AS cq LEFT JOIN cms_question_category AS cqc ON cqc.pk=cq.question_category_fk {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<QuestionList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsQuestionService][FindQuestionList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
