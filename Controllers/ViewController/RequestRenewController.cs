using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.RequestRenew;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class RequestRenewController : BaseController
    {
		public void SetSelect()
		{
		}

		[MenuFilter(355, 3)]
		[TranslatorUIFilter("RequestRenew")]
        public IActionResult Index(RequestRenewFilter filter, int page = 1)
        {
			SetSelect();
            RequestRenewVm model = new()
            {
                filter = filter ?? new RequestRenewFilter()
            };

            try
            {
                model.list = RequestRenewBiz.GetRequestRenewList(model.filter);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(355, 3)]
		[TranslatorUIFilter("RequestRenew")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            BorrowRequestDto dto = RequestRenewBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(BorrowRequestDto req)
        {
            SetSelect();
            try
            {
				RequestRenewBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[MenuFilter(355, 3)]
		[TranslatorUIFilter("RequestRenew")]		
		public IActionResult Review(int pk)
        {
			RequestRenewReview model = RequestRenewBiz.GetReview(pk);
            return View(model);
        }

        public IActionResult PostReview(BorrowRequestDto req, bool result)
        {
            try
            {
                RequestRenewBiz.RenewalVerify(req.pk, result);
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
