using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.EndTradeAccount;
using X.PagedList;

namespace NTKServer.Controllers
{
	public class EndTradeAccountController : BaseController
    {
		public void SetSelect()
		{
			ViewBag.market = SysMarketBiz.GetDropDownList(GetLanguage());
			ViewBag.borrowType = BorrowPlanBiz.GetSelectListItems();
		}

		[MenuFilter(388, 2)]
		[TranslatorUIFilter("EndTradeAccount")]
        public IActionResult Index(EndTradeAccountFilter filter, int page = 1)
        {
			SetSelect();

            EndTradeAccountVm model = new()
            {
                filter = filter ?? new EndTradeAccountFilter()
            };

            try
            {
                model.list = EndTradeAccountBiz.GetEndTradeAccountList(model.filter).ToPagedList(page, pageSize);
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
