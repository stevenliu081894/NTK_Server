using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.Wallet;
using NTKServer.ViewModels.WalletRecord;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class WalletController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(264, 5)]
		[TranslatorUIFilter("Wallet")]
        public IActionResult Index(WalletFilter filter, int page = 1)
        {
            SetFilterSelect();

            WalletVm model = new()
            {
                filter = filter ?? new WalletFilter()
            };

            try
            {
                model.list = WalletBiz.GetWalletList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(264, 5)]
		[TranslatorUIFilter("Wallet")]		
		public IActionResult Edit(int member_fk)
        {
            SetSelect();
			WalletEditVm dto = WalletBiz.GetEditVm(member_fk);
            return View(dto);
        }

        public IActionResult PostEdit(WalletDto req)
        {
            SetSelect();
            try
            {
				WalletBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[MenuFilter(264, 5)]
		[TranslatorUIFilter("Wallet")]
		public IActionResult ChangeException(int member_fk)
		{
			SetSelect();
			WalletChangeExceptionVm dto = WalletBiz.GetChangeException(member_fk);
			return View(dto);
		}

		public IActionResult PostChangeException(WalletChangeExceptionVm req)
		{
			try
			{
                WalletRechargeBiz.ChangeException(req);
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
