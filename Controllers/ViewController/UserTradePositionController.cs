using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.UserTradePosition;
using System.Data;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class UserTradePositionController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[UseFilter(395, 2)]
		[TranslatorUIFilter("UserTradePosition")]
        public IActionResult Index(string subaccount, UserTradePositionFilter filter)
        {
			if (string.IsNullOrEmpty(subaccount) == true)
			{
				throw new AppException(210, "illegal_operation");
			}

			SetFilterSelect();

            UserTradePositionVm model = new()
            {
                filter = filter ?? new UserTradePositionFilter()
            };

            try
            {
                model.list = UserTradePositionBiz.GetUserTradePositionList(subaccount, model.filter);
                ViewBag.account = subaccount;
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
