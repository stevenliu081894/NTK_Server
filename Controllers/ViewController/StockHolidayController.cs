using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.StockHoliday;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class StockHolidayController : BaseController
    {
        public void SetSelect()
        {
            ViewBag.market = SysMarketBiz.GetDropDownList(GetLanguage());
        }

        [MenuFilter(100, 8)]
		[TranslatorUIFilter("StockHoliday")]
        public IActionResult Index(StockHolidayFilter filter, int page = 1)
        {
            SetSelect();
            StockHolidayVm model = new()
            {
                filter = filter ?? new StockHolidayFilter()
            };

            try
            {
                model.list = StockHolidayBiz.GetStockHolidayList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(100, 8)]
		[TranslatorUIFilter("StockHoliday")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            StockHolidayDto dto = StockHolidayBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(StockHolidayDto req)
        {
            SetSelect();
            try
            {
				StockHolidayBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(100, 8)]
		[TranslatorUIFilter("StockHoliday")]
        public IActionResult Create()
        {
            SetSelect();
            StockHolidayDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(StockHolidayDto req)
        {
            SetSelect();
            try
            {
				StockHolidayBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(100, 8)]
        public IActionResult Delete(int pk)
        {
            try
            {
                StockHolidayBiz.Delete(pk);
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
