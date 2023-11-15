using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.AdminBank;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class AdminBankController : BaseController
    {
        public void SetSelect()
        {
        }

        [MenuFilter(302, 5)]
		[TranslatorUIFilter("AdminBank")]
        public IActionResult Index(AdminBankFilter filter, int page = 1)
        {
            SetSelect();
            AdminBankVm model = new()
            {
                filter = filter ?? new AdminBankFilter()
            };

            try
            {
                model.list = AdminBankBiz.GetAdminBankList(model.filter);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(303, 5)]
		[TranslatorUIFilter("AdminBank")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            AdminBankDto dto = AdminBankBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(AdminBankDto req)
        {
            SetSelect();
            try
            {
				AdminBankBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(435, 5)]
		[TranslatorUIFilter("AdminBank")]
        public IActionResult Create()
        {
            SetSelect();
            AdminBankDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(AdminBankDto req)
        {
            SetSelect();
            try
            {
				AdminBankBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(304, 5)]
        public IActionResult Delete(int pk)
        {
            try
            {
                AdminBankBiz.Delete(pk);
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
