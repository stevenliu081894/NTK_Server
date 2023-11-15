using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.Question;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class QuestionController : BaseController
    {
        public void SetSelect(string? lang)
        {
            ViewBag.langDropdown = MultiLangBiz.FindSelectList();
            ViewBag.questionCategory = QuestionCategoryBiz.GetDropDownList(lang == null ? "" : lang);
        }

        [MenuFilter(125, 7)]
		[TranslatorUIFilter("Question")]
        public IActionResult Index(QuestionFilter filter, int page = 1)
        {
            SetSelect("");
            QuestionVm model = new()
            {
                filter = filter ?? new QuestionFilter()
            };
            try
            {
                model.list = QuestionBiz.GetQuestionList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(125, 7)]
		[TranslatorUIFilter("Question")]		
		public IActionResult Edit(int pk)
        {
            SetSelect("");
            CmsQuestionDto dto = QuestionBiz.Get(pk);
			return View(dto);
        }

		[MenuFilter(125, 7)]
		[TranslatorUIFilter("Question")]
		public IActionResult PostEdit(CmsQuestionDto req)
        {
            SetSelect("");
            try
            {
                req.answer = UploadImageLib.RemoveHostName(req.answer);
				QuestionBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(125, 7)]
		[TranslatorUIFilter("Question")]
        public IActionResult Create(string? lang)
        {
            SetSelect(lang);
            CmsQuestionDto dto = new();			
			return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            try
            {
                var img_path = await UploadBiz.UploadImage(upload, FileManagementLib.Folder.article);
                var success = new UploadSuccess
                {
                    uploaded = 1,
                    url = img_path
                };

                return new JsonResult(success);
            }
            catch (AppException ex)
            {
                ShowError(ex.Message);
                return View();
            }
        }

		[MenuFilter(125, 7)]
		[TranslatorUIFilter("Question")]
		public IActionResult PostCreate(CmsQuestionDto req)
        {
            SetSelect("");
            try
            {
                req.answer = UploadImageLib.RemoveHostName(req.answer);
				QuestionBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(125, 7)]
        public IActionResult Delete(int pk)
        {
            try
            {
                QuestionBiz.Delete(pk);
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
