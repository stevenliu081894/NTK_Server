using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.Promotion;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class PromotionController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(145, 7)]
		[TranslatorUIFilter("Promotion")]
        public IActionResult Index(PromotionFilter filter, int page = 1)
        {
            SetFilterSelect();

            PromotionVm model = new()
            {
                filter = filter ?? new PromotionFilter()
            };

            try
            {
                model.list = PromotionBiz.GetPromotionList(model.filter).ToPagedList(page, pageSize);
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
		}

		[UseFilter(145, 7)]
		[TranslatorUIFilter("Promotion")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            CmsPromotionDto dto = PromotionBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(CmsPromotionDto req)
        {
            SetSelect();
            try
            {
                req.topic_content = UploadImageLib.RemoveHostName(req.topic_content);
				PromotionBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(145, 7)]
		[TranslatorUIFilter("Promotion")]
        public IActionResult Create()
        {
            SetSelect();
            CmsPromotionDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(CmsPromotionDto req)
        {
            SetSelect();
            try
            {
                req.topic_content = UploadImageLib.RemoveHostName(req.topic_content);
				PromotionBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
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

		[UseFilter(145, 7)]
        public IActionResult Delete(int pk)
        {
            try
            {
                PromotionBiz.Delete(pk);
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
