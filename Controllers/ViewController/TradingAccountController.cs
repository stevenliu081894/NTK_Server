using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.TradingAccount;


namespace NTKServer.Controllers
{
    public class TradingAccountController : BaseController
    {
		public void SetSelect()
		{
            ViewBag.market = SysMarketBiz.GetDropDownList(GetLanguage());
            ViewBag.borrowType = BorrowPlanBiz.GetSelectListItems();
			ViewBag.status = from AccountStatusType status in Enum.GetValues(typeof(AccountStatusType))
							 select new SelectListItem
							 {
								 Text = ConvertEnum.ConvertAccountStatus((int)status),
								 Value = ((int)status).ToString()
							 };
		}

		[MenuFilter(390, 2)]
		[TranslatorUIFilter("TradingAccount")]
        public IActionResult Index(TradingAccountFilter filter)
        {
			SetSelect();
            TradingAccountVm model = new()
            {
                filter = filter ?? new TradingAccountFilter()
            };

            try
            {
                model.list = TradingAccountBiz.GetTradingAccountList(model.filter);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }
     

		[UseFilter(390, 2)]
		[TranslatorUIFilter("TradingAccount")]		
		public IActionResult Edit(string sub_account)
        {
            SetSelect();
			VwTradeAccountDto dto = TradingAccountBiz.GetFromView(sub_account);
            return View(dto);
        }

        public IActionResult PostEdit(TradeAccountDto req)
        {
            SetSelect();
            try
            {
				TradingAccountBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }
    }	
}
