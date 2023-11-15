using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.Addfinancing;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class AddfinancingController : BaseController
    {
        public void SetSelect()
        {
        }

        [MenuFilter(325, 3)]
		[TranslatorUIFilter("Addfinancing")]
        public IActionResult Index(AddfinancingFilter filter, int page = 1)
        {
            SetSelect();
            AddfinancingVm model = new()
            {
                filter = filter ?? new AddfinancingFilter()
            };

            try
            {
                model.list = AddfinancingBiz.GetAddfinancingList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(325, 3)]
		[TranslatorUIFilter("Addfinancing")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            BorrowAddfinancingDto dto = AddfinancingBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(BorrowAddfinancingDto req)
        {
            SetSelect();
            try
            {
				AddfinancingBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }


		[UseFilter(325, 3)]
		[TranslatorUIFilter("Addfinancing")]		
		public IActionResult Review(int pk)
        {
            AddfinancingReview model = AddfinancingBiz.GetReview(pk);
            return View(model);
        }

        public IActionResult PostReview(BorrowAddfinancingDto req, bool result)
        {
            try
            {
                AddfinancingBiz.ExpandBorrowVerify(req.pk,result);
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
