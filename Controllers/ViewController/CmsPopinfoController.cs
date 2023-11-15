using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Data.Enums;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.CmsPopinfo;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class CmsPopinfoController : BaseController
    {
		public void SetSelect()
		{
			ViewBag.langDropdown = MultiLangBiz.FindSelectList();
			ViewBag.sizeDropdown = from CmsPopInfoEnum size in Enum.GetValues(typeof(CmsPopInfoEnum))
								   select new SelectListItem
								   {
									   Text = CmsPopInfoConvertEnum.ConvertSize((int)size),
									   Value = ((int)size).ToString()
								   };
		}

		[MenuFilter(209, 7)]
		[TranslatorUIFilter("CmsPopinfo")]
        public IActionResult Index(CmsPopinfoFilter filter, int page = 1)
        {
			SetSelect();
            CmsPopinfoVm model = new()
            {
                filter = filter ?? new CmsPopinfoFilter()
            };

            try
            {
                model.list = CmsPopinfoBiz.GetCmsPopinfoList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(209, 7)]
		[TranslatorUIFilter("CmsPopinfo")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            CmsPopinfoDto dto = CmsPopinfoBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(CmsPopinfoDto req)
        {
            SetSelect();
            try
            {
				CmsPopinfoBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(209, 7)]
		[TranslatorUIFilter("CmsPopinfo")]
        public IActionResult Create()
        {
            SetSelect();
            CmsPopinfoDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(CmsPopinfoDto req)
        {
            SetSelect();
            try
            {
				CmsPopinfoBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(209, 7)]
        public IActionResult Delete(int pk)
        {
            try
            {
                CmsPopinfoBiz.Delete(pk);
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
