using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.StockUs;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class StockUsController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(102, 8)]
		[TranslatorUIFilter("StockUs")]
        public IActionResult Index(StockUsFilter filter, int page = 1)
        {
            SetFilterSelect();

            StockUsVm model = new()
            {
                filter = filter ?? new StockUsFilter()
            };

            try
            {
                model.list = StockUsBiz.GetStockUsList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(102, 8)]
		[TranslatorUIFilter("StockUs")]		
		public IActionResult Edit(string stock_code)
        {
            SetSelect();
            StockUsDto dto = StockUsBiz.Get(stock_code);
            return View(dto);
        }

        public IActionResult PostEdit(StockUsDto req)
        {
            SetSelect();
            try
            {
				StockUsBiz.PostEdit(req);               
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
