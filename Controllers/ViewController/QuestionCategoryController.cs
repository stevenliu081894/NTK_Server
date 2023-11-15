using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.QuestionCategory;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class QuestionCategoryController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(139, 7)]
		[TranslatorUIFilter("QuestionCategory")]
        public IActionResult Index(QuestionCategoryFilter filter, int page = 1)
        {
            SetFilterSelect();

            QuestionCategoryVm model = new()
            {
                filter = filter ?? new QuestionCategoryFilter()
            };

            try
            {
                model.list = QuestionCategoryBiz.GetQuestionCategoryList(model.filter).ToPagedList(page, pageSize);
                
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

        public void SetSelect()
        {
            ViewBag.langDropdown = MultiLangBiz.FindSelectList();
		}

		[UseFilter(139, 7)]
		[TranslatorUIFilter("QuestionCategory")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            CmsQuestionCategoryDto dto = QuestionCategoryBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(CmsQuestionCategoryDto req)
        {
            SetSelect();
            try
            {
				QuestionCategoryBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(139, 7)]
		[TranslatorUIFilter("QuestionCategory")]
        public IActionResult Create()
        {
            SetSelect();
            CmsQuestionCategoryDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(CmsQuestionCategoryDto req)
        {
            SetSelect();
            try
            {
				QuestionCategoryBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(139, 7)]
        public IActionResult Delete(int pk)
        {
            try
            {
                QuestionCategoryBiz.Delete(pk);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return RedirectToAction("Index");
            }
        }


    }	
}
