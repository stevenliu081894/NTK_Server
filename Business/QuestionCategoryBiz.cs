using DB.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.QuestionCategory;

namespace NTKServer.Business
{
    public class QuestionCategoryBiz
    {
        #region CRUD
		public static List<QuestionCategoryList> GetQuestionCategoryList(QuestionCategoryFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsQuestionCategoryService.FindQuestionCategoryList(whereSql);
        }

        public static CmsQuestionCategoryDto Get(int pk)
        {
            return CmsQuestionCategoryService.Find(pk);
        }

        public async static void PostCreate(CmsQuestionCategoryDto req)
        {
            var image_url = await UploadImageLib.UploadImage(FileManagementLib.Folder.article, req.image);
            req.icon = image_url;
            if (CmsQuestionCategoryService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(CmsQuestionCategoryDto req)
        {
            if( CmsQuestionCategoryService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            CmsQuestionCategoryService.Remove(pk);
        }

        #endregion

        #region ViewModel		

        public static List<SelectListItem> GetDropDownList(string lang)
        {
            var list = new List<SelectListItem>();
            var categories = CmsQuestionCategoryService.FindDropDown(lang);
            foreach (var category in categories)
            {
                list.Add(new SelectListItem()
                {
                    Value = $"{category.pk}",
                    Text = category.label
                });
            }
            return list;
        }

        #endregion
    }
}
