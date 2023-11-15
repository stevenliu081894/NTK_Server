using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.EndWalletWithdraw;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class EndWalletWithdrawController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(295, 5)]
		[TranslatorUIFilter("EndWalletWithdraw")]
        public IActionResult Index(EndWalletWithdrawFilter filter, int page = 1)
        {
            SetFilterSelect();

            EndWalletWithdrawVm model = new()
            {
                filter = filter ?? new EndWalletWithdrawFilter()
            };

            try
            {
                model.list = EndWalletWithdrawBiz.GetEndWalletWithdrawList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

        public void SetSelect()
        {
		}

		[UseFilter(295, 5)]
		[TranslatorUIFilter("EndWalletWithdraw")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
			EndWalletWithdrawEditVm dto = EndWalletWithdrawBiz.GetEditVm(pk);
            return View(dto);
        }

        public IActionResult PostEdit(WalletWithdrawDto req)
        {
            SetSelect();
            try
            {
				EndWalletWithdrawBiz.PostEdit(req);               
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
