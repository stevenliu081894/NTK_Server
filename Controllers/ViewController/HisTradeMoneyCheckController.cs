using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.HisTradeMoneyCheck;
using NTKServer.ViewModels.TradeMoneyCheck;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class HisTradeMoneyCheckController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(261, 2)]
		[TranslatorUIFilter("HisTradeMoneyCheck")]
        public IActionResult Index(HisTradeMoneyCheckFilter filter, int page = 1)
        {
            SetFilterSelect();

            HisTradeMoneyCheckVm model = new()
            {
                filter = filter ?? new HisTradeMoneyCheckFilter()
            };

            try
            {
                model.list = HisTradeMoneyCheckBiz.GetHisTradeMoneyCheckList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[MenuFilter(261, 2)]
		[TranslatorUIFilter("HisTradeMoneyCheck")]		
		public IActionResult Review(int pk)
        {
            TradeMoneyCheckReview model = HisTradeMoneyCheckBiz.GetReview(pk);
            return View(model);
        }
    }	
}
