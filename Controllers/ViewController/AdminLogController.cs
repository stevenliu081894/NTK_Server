using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.AdminLog;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class AdminLogController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(240, 13)]
		[TranslatorUIFilter("AdminLog")]
        public IActionResult Index(AdminLogFilter filter, int page = 1)
        {
            SetFilterSelect();

            AdminLogVm model = new()
            {
                filter = filter ?? new AdminLogFilter()
            };

            try
            {
                model.list = AdminLogBiz.GetAdminLogList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }
    }	
}
