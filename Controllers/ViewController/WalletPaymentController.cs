using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.WalletPayment;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class WalletPaymentController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(290, 5)]
		[TranslatorUIFilter("WalletPayment")]
        public IActionResult Index(WalletPaymentFilter filter, int page = 1)
        {
            SetFilterSelect();

            WalletPaymentVm model = new()
            {
                filter = filter ?? new WalletPaymentFilter()
            };

            try
            {
                model.list = WalletPaymentBiz.GetWalletPaymentList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(290, 5)]
		[TranslatorUIFilter("WalletPayment")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            WalletPaymentDto dto = WalletPaymentBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(WalletPaymentDto req)
        {
            SetSelect();
            try
            {
				WalletPaymentBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(290, 5)]
		[TranslatorUIFilter("WalletPayment")]
        public IActionResult Create()
        {
            SetSelect();
			WalletPaymentDto dto = new()
			{
				status = true
			};

			return View(dto);
        }

        public IActionResult PostCreate(WalletPaymentDto req)
        {
            SetSelect();
            try
            {
				WalletPaymentBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(289, 5)]
        public IActionResult Delete(int pk)
        {
            try
            {
                WalletPaymentBiz.Delete(pk);
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
