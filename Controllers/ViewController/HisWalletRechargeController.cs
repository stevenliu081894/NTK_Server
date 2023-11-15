using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.HisWalletRecharge;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class HisWalletRechargeController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(273, 5)]
		[TranslatorUIFilter("HisWalletRecharge")]
        public IActionResult Index(HisWalletRechargeFilter filter, int page = 1)
        {
            SetFilterSelect();
            HisWalletRechargeVm model = new()
            {
                filter = filter ?? new HisWalletRechargeFilter()
            };

            try
            {
                model.list = HisWalletRechargeBiz.GetHisWalletRechargeList(model.filter).ToPagedList(page, pageSize);
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
