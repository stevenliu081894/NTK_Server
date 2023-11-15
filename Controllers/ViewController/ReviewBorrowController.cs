using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.ReviewBorrow;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class ReviewBorrowController : BaseController
    {
        public void SetSelect()
        {
        }

        [MenuFilter(322, 3)]
		[TranslatorUIFilter("ReviewBorrow")]
        public IActionResult Index(ReviewBorrowFilter filter, int page = 1)
        {
            SetSelect();
            ReviewBorrowVm model = new()
            {
                filter = filter ?? new ReviewBorrowFilter()
            };

            try
            {
                model.list = ReviewBorrowBiz.GetReviewBorrowList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }
       

		[MenuFilter(322, 3)]
		[TranslatorUIFilter("ReviewBorrow")]		
		public IActionResult Review(int pk)
        {
            SetSelect();
            BorrowDto dto = ReviewBorrowBiz.Get(pk);
            return View(dto);
        }

        [MenuFilter(322, 3)]
        [TranslatorUIFilter("ReviewBorrow")]
        public IActionResult PostReview(BorrowDto req, bool result)
        {
            SetSelect();
            try
            {
                ReviewBorrowBiz.BorrowApplyVerify(req.pk,result);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                BorrowDto dto = ReviewBorrowBiz.Get(req.pk);
                return View("Review", dto);
            }
        }
    }	
}
