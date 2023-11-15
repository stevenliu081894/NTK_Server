using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.Question;

namespace NTKServer.Business
{
    public class QuestionBiz
    {
        #region CRUD
		public static List<QuestionList> GetQuestionList(QuestionFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsQuestionService.FindQuestionList(whereSql);
        }

        public static CmsQuestionDto Get(int pk)
        {
            return CmsQuestionService.Find(pk);
        }

        public static void PostCreate(CmsQuestionDto req)
        {
			if (req.question_category_fk == 0)
			{
				throw new AppException(3010, "record_update_false");
			}
			if (CmsQuestionService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(CmsQuestionDto req)
        {
            if (req.question_category_fk == 0)
            {
				throw new AppException(3010, "record_update_false");
			}
			if ( CmsQuestionService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            CmsQuestionService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
