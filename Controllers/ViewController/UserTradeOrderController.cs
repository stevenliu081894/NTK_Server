using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.UserTradeOrder;
using X.PagedList;

namespace NTKServer.Controllers
{
	public class UserTradeOrderController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[UseFilter(393, 2)]
		[TranslatorUIFilter("UserTradeOrder")]
        public IActionResult Index(string subaccount, UserTradeOrderFilter filter)
        {
			if (string.IsNullOrEmpty(subaccount) == true)
			{
				throw new AppException(210, "illegal_operation");
			}

			SetFilterSelect();

            UserTradeOrderVm model = new()
            {
                filter = filter ?? new UserTradeOrderFilter()
            };

            try
            {
                model.list = UserTradeOrderBiz.GetUserTradeOrderList(subaccount, model.filter);
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
