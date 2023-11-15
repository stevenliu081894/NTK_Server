using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.WalletWithdraw;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class WalletWithdrawController : BaseController
    {
       
		[MenuFilter(294, 5)]
		[TranslatorUIFilter("WalletWithdraw")]
        public IActionResult Index(WalletWithdrawFilter filter, int page = 1)
        {
            WalletWithdrawVm model = new()
            {
                filter = filter ?? new WalletWithdrawFilter()
            };
            try
            {
                model.list = WalletWithdrawBiz.GetWalletWithdrawList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[MenuFilter(294, 5)]
		[TranslatorUIFilter("WalletWithdraw")]		
		public IActionResult Review(int pk)
        {
            WalletWithdrawReview model = WalletWithdrawBiz.GetReview(pk);
            return View(model);
        }

		[MenuFilter(294, 5)]
		[TranslatorUIFilter("WalletWithdraw")]
		public IActionResult PostReview(WalletWithdrawDto req, bool result)
        {
            try
            {
				WalletWithdrawBiz.VerifyWalletWithdraw(req,result);
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
