using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.RequestStop;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class RequestStopController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(366, 3)]
		[TranslatorUIFilter("RequestStop")]
        public IActionResult Index(RequestStopFilter filter, int page = 1)
        {
            SetFilterSelect();

            RequestStopVm model = new()
            {
                filter = filter ?? new RequestStopFilter()
            };

            try
            {
                model.list = RequestStopBiz.GetRequestStopList(model.filter);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[MenuFilter(366, 3)]
		[TranslatorUIFilter("RequestStop")]		
		public IActionResult Review(int pk)
        {
            RequestStopReview model = RequestStopBiz.GetReview(pk);
            return View(model);
        }

        public IActionResult PostReview(BorrowRequestDto req, bool result)
        {
            try
            {
                RequestStopBiz.TerminateVerify(GetUser().pk, req.pk, result);
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
