using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.BorrowAddmoney;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class BorrowAddmoneyController : BaseController
    {
		public void SetSelect()
		{
			ViewBag.market = SysMarketBiz.GetDropDownList(GetLanguage());
		}

		[MenuFilter(329, 3)]
		[TranslatorUIFilter("BorrowAddmoney")]
        public IActionResult Index(BorrowAddmoneyFilter filter, int page = 1)
        {
			SetSelect();
            BorrowAddmoneyVm model = new()
            {
                filter = filter ?? new BorrowAddmoneyFilter()
            };

            try
            {
                model.list = BorrowAddmoneyBiz.GetBorrowAddmoneyList(model.filter);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[MenuFilter(329, 3)]
		[TranslatorUIFilter("BorrowAddmoney")]		
		public IActionResult Review(int pk)
        {
            BorrowAddmoneyReview model = BorrowAddmoneyBiz.GetReview(pk);
            return View(model);
        }

        public IActionResult PostReview(BorrowAddmoneyDto req, bool result)
        {
            try
            {
                BorrowAddmoneyBiz.ReviewApprove(req.pk, result);
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
