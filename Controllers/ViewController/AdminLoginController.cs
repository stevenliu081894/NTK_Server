using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.AdminLogin;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class AdminLoginController : BaseController
    {
        private void SetFilterSelect()
        {			
        }

		[MenuFilter(239, 13)]
		[TranslatorUIFilter("AdminLogin")]
        public IActionResult Index(AdminLoginFilter filter, int page = 1)
        {
            SetFilterSelect();

            AdminLoginVm model = new()
            {
                filter = filter ?? new AdminLoginFilter()
            };

            try
            {
                model.list = AdminLoginBiz.GetAdminLoginList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(239, 13)]
        public IActionResult Delete(int pk)
        {
            try
            {
                AdminLoginBiz.Delete(pk);
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
