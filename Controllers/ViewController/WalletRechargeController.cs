using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.WalletRecharge;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class WalletRechargeController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(274, 5)]
		[TranslatorUIFilter("WalletRecharge")]
        public IActionResult Index(WalletRechargeFilter filter, int page = 1)
        {
            SetFilterSelect();

            WalletRechargeVm model = new()
            {
                filter = filter ?? new WalletRechargeFilter()
            };

            try
            {
                model.list = WalletRechargeBiz.GetWalletRechargeList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[MenuFilter(274, 5)]
		[TranslatorUIFilter("WalletRecharge")]		
		public IActionResult Review(int pk)
        {
            WalletRechargeReview model = WalletRechargeBiz.GetReview(pk);
            return View(model);
        }

        public IActionResult PostReview(WalletRechargeDto req, bool result)
        {
            try
            {
                WalletRechargeBiz.RechargeVerify(req.pk, result, GetUser().pk, req.reject_result);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return RedirectToAction("Index");
            }
        }

    }	
}
