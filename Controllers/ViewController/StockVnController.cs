using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.StockVn;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class StockVnController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(101, 8)]
		[TranslatorUIFilter("StockVn")]
        public IActionResult Index(StockVnFilter filter, int page = 1)
        {
            SetFilterSelect();

            StockVnVm model = new()
            {
                filter = filter ?? new StockVnFilter()
            };

            try
            {
                model.list = StockVnBiz.GetStockVnList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(101, 8)]
		[TranslatorUIFilter("StockVn")]		
		public IActionResult Edit(string stock_code)
        {
            SetSelect();
            StockVnDto dto = StockVnBiz.Get(stock_code);
            return View(dto);
        }

        public IActionResult PostEdit(StockVnDto req)
        {
            SetSelect();
            try
            {
				StockVnBiz.PostEdit(req);               
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
