using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.TradeCancel;

namespace NTKServer.Controllers
{
	public class TradeCancelController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[UseFilter(367, 2)]
		[TranslatorUIFilter("TradeCancel")]
        public IActionResult Index(string subAccount, TradeCancelFilter filter)
        {
            if (string.IsNullOrEmpty(subAccount) == true)
            {
                throw new AppException(210, "illegal_operation");
            }

            SetFilterSelect();

            TradeCancelVm model = new()
            {
                filter = filter ?? new TradeCancelFilter()
            };

            try
            {
                model.list = TradeCancelBiz.GetTradeCancelList(subAccount, model.filter);
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
