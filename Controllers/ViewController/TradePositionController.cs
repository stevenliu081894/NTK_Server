using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.TradePosition;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class TradePositionController : BaseController
    {
		public void SetSelect()
		{
			ViewBag.market = SysMarketBiz.GetDropDownList(GetLanguage());
		}

		[MenuFilter(394, 2)]
		[TranslatorUIFilter("TradePosition")]
        public IActionResult Index(TradePositionFilter filter, int page = 1)
        {
			SetSelect();
            TradePositionVm model = new()
            {
                filter = filter ?? new TradePositionFilter()
            };

            try
            {
                model.list = TradePositionBiz.GetTradePositionList(model.filter).ToPagedList(page, pageSize);
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
