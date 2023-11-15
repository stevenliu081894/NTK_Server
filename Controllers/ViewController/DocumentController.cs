using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.ViewModels.Document;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class DocumentController : BaseController
    {
		public void SetSelect()
		{
			ViewBag.langDropdown = MultiLangBiz.FindSelectList();
		}

		[MenuFilter(158, 7)]
		[TranslatorUIFilter("Document")]
        public IActionResult Index(DocumentFilter filter, int page = 1)
        {
			SetSelect();
            DocumentVm model = new()
            {
                filter = filter ?? new DocumentFilter()
            };

            try
            {
                model.list = DocumentBiz.GetDocumentList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(158, 7)]
		[TranslatorUIFilter("Document")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            CmsDocumentDto dto = DocumentBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(CmsDocumentDto req)
        {
            SetSelect();
            try
            {
                req.content = UploadImageLib.RemoveHostName(req.content);
				DocumentBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(158, 7)]
        [TranslatorUIFilter("Document")]
        public IActionResult Copy(int pk)
        {
            SetSelect();
            CmsDocumentDto dto = DocumentBiz.Get(pk);
            return View(dto);
        }

        [TranslatorUIFilter("Document")]
        public IActionResult PostCopy(CmsDocumentDto req)
        {
            SetSelect();
            try
            {
                req.content = UploadImageLib.RemoveHostName(req.content);
                DocumentBiz.PostCopy(req);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Copy", req);
            }
        }

        [UseFilter(158, 7)]
		[TranslatorUIFilter("Document")]
        public IActionResult Create()
        {
            SetSelect();
            CmsDocumentDto dto = new CmsDocumentDto { status = true};
            return View(dto);
        }

        public IActionResult PostCreate(CmsDocumentDto req)
        {
            SetSelect();
            try
            {
                req.content = UploadImageLib.RemoveHostName(req.content);
				DocumentBiz.PostCreate(req);
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

		[UseFilter(158, 7)]
        public IActionResult Delete(int pk)
        {
            try
            {
                DocumentBiz.Delete(pk);
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
