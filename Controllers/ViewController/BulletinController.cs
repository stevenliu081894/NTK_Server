using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.Bulletin;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class BulletinController : BaseController
    {
		public void SetSelect()
		{
			ViewBag.langDropdown = MultiLangBiz.FindSelectList();
		}

		[MenuFilter(143, 7)]
		[TranslatorUIFilter("Bulletin")]
        public IActionResult Index(BulletinFilter filter, int page = 1)
        {
			SetSelect();
            BulletinVm model = new()
            {
                filter = filter ?? new BulletinFilter()
            };

            try
            {
                model.list = BulletinBiz.GetBulletinList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(143, 7)]
		[TranslatorUIFilter("Bulletin")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            CmsBulletinDto dto = BulletinBiz.Get(pk);
			return View(dto);
        }

        public IActionResult PostEdit(CmsBulletinDto req)
        {
            SetSelect();
            try
            {
                req.topic_content = UploadImageLib.RemoveHostName(req.topic_content);
				BulletinBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(143, 7)]
		[TranslatorUIFilter("Bulletin")]
        public IActionResult Create()
        {
            SetSelect();
            CmsBulletinDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(CmsBulletinDto req)
        {
            SetSelect();
            try
            {
                req.topic_content = UploadImageLib.RemoveHostName(req.topic_content);
				BulletinBiz.PostCreate(req);
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

		[UseFilter(143, 7)]
        public IActionResult Delete(int pk)
        {
            try
            {
                BulletinBiz.Delete(pk);
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
