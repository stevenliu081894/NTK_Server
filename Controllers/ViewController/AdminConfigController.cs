using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.AdminConfig;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class AdminConfigController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(89, 9)]
		[TranslatorUIFilter("AdminConfig")]
        public IActionResult Index(AdminConfigFilter filter, int page = 1)
        {
            SetFilterSelect();

            AdminConfigVm model = new()
            {
                filter = filter ?? new AdminConfigFilter()
            };

            try
            {
                model.list = AdminConfigBiz.GetAdminConfigList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(89, 9)]
		[TranslatorUIFilter("AdminConfig")]		
		public IActionResult Edit(string name)
        {
            SetSelect();
            AdminConfigDto dto = AdminConfigBiz.Get(name);
            return View(dto);
        }

        public IActionResult PostEdit(AdminConfigDto req)
        {
            SetSelect();
            try
            {
				AdminConfigBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(89, 9)]
		[TranslatorUIFilter("AdminConfig")]
        public IActionResult Create()
        {
            SetSelect();
            AdminConfigDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(AdminConfigDto req)
        {
            SetSelect();
            try
            {
				AdminConfigBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }
    }	
}
