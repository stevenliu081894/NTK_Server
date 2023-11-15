using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.TradeMoneyCheck;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class TradeMoneyCheckController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(260, 2)]
		[TranslatorUIFilter("TradeMoneyCheck")]
        public IActionResult Index(TradeMoneyCheckFilter filter, int page = 1)
        {
            SetFilterSelect();

            TradeMoneyCheckVm model = new()
            {
                filter = filter ?? new TradeMoneyCheckFilter()
            };

            try
            {
                model.list = TradeMoneyCheckBiz.GetTradeMoneyCheckList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }


		[MenuFilter(260, 2)]
		[TranslatorUIFilter("TradeMoneyCheck")]		
		public IActionResult Review(int pk)
        {
            TradeMoneyCheckReview model = TradeMoneyCheckBiz.GetReview(pk);
            return View(model);
        }

        
        public IActionResult PostReview(TradeMoneyCheckDto req, bool result)
        {
            try
            {
                TradeMoneyCheckBiz.WithdrawVerify(req.pk, result);

                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Review", req);
            }
        }

    }	
}
