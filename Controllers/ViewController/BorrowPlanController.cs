using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.BorrowPlan;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class BorrowPlanController : BaseController
    {
		public void SetSelect()
		{
			ViewBag.market = SysMarketBiz.GetDropDownList(GetLanguage());
		}

		[MenuFilter(1, 3)]
		[TranslatorUIFilter("BorrowPlan")]
        public IActionResult Index(BorrowPlanFilter filter, int page = 1)
        {
			SetSelect();
            BorrowPlanVm model = new()
            {
                filter = filter ?? new BorrowPlanFilter(),
            };

            try
            {
                model.list = BorrowPlanBiz.GetBorrowPlanList(model.filter);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(1, 3)]
		[TranslatorUIFilter("BorrowPlan")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            BorrowPlanDto dto = BorrowPlanBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(BorrowPlanDto req)
        {
            SetSelect();
            try
            {
				BorrowPlanBiz.PostEdit(req);               
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
