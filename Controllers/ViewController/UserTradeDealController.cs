using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.UserTradeDeal;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class UserTradeDealController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[UseFilter(396, 2)]
		[TranslatorUIFilter("UserTradeDeal")]
        public IActionResult Index(string subaccount, UserTradeDealFilter filter)
        {
			if (string.IsNullOrEmpty(subaccount) == true)
			{
				throw new AppException(210, "illegal_operation");
			}

			SetFilterSelect();

            UserTradeDealVm model = new()
            {
                filter = filter ?? new UserTradeDealFilter()
            };

            try
            {
                model.list = UserTradeDealBiz.GetUserTradeDealList(subaccount, model.filter);
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
