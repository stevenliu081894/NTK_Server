using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.CmsSupport;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class CmsSupportController : BaseController
    {
        private void SetSelect()
        {
			ViewBag.langDropdown = MultiLangBiz.FindSelectList();
		}

		[MenuFilter(23, 9)]
		[TranslatorUIFilter("CmsSupport")]
        public IActionResult Index(CmsSupportFilter filter, int page = 1)
        {
            SetSelect();
            CmsSupportVm model = new()
            {
                filter = filter ?? new CmsSupportFilter()
            };

            try
            {
                model.list = CmsSupportBiz.GetCmsSupportList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(23, 9)]
		[TranslatorUIFilter("CmsSupport")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            CmsSupportDto dto = CmsSupportBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(CmsSupportDto req)
        {
            SetSelect();
            try
            {
				CmsSupportBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(23, 9)]
		[TranslatorUIFilter("CmsSupport")]
        public IActionResult Create()
        {
            SetSelect();
            CmsSupportDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(CmsSupportDto req)
        {
            SetSelect();
            try
            {
				CmsSupportBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(23, 9)]
        public IActionResult Delete(int pk)
        {
            try
            {
                CmsSupportBiz.Delete(pk);
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
