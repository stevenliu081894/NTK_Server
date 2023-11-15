using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.SysCountry;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class SysCountryController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(424, 9)]
		[TranslatorUIFilter("SysCountry")]
        public IActionResult Index(SysCountryFilter filter, int page = 1)
        {
            SetFilterSelect();

            SysCountryVm model = new()
            {
                filter = filter ?? new SysCountryFilter()
            };

            try
            {
                model.list = SysCountryBiz.GetSysCountryList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(424, 9)]
		[TranslatorUIFilter("SysCountry")]		
		public IActionResult Edit(string pk)
        {
            SetSelect();
            SysCountryDto dto = SysCountryBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(SysCountryDto req)
        {
            SetSelect();
            try
            {
				SysCountryBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(424, 9)]
		[TranslatorUIFilter("SysCountry")]
        public IActionResult Create()
        {
            SetSelect();
            SysCountryDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(SysCountryDto req)
        {
            SetSelect();
            try
            {
				SysCountryBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(424, 9)]
        public IActionResult Delete(string pk)
        {
            try
            {
                SysCountryBiz.Delete(pk);
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
