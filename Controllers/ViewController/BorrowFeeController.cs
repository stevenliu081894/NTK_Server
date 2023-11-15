using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.BorrowFee;
using System.Data;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class BorrowFeeController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(281, 5)]
		[TranslatorUIFilter("BorrowFee")]
        public IActionResult Index(BorrowFeeFilter filter, int page = 1)
        {
            SetFilterSelect();

            BorrowFeeVm model = new()
            {
                filter = filter ?? new BorrowFeeFilter()
            };

            try
            {               
                model.list = BorrowFeeBiz.GetBorrowFeeList(model.filter).ToPagedList(page, pageSize);
                
                decimal total = 0;
                if(model.list != null && model.list.Count > 0) 
                { 
                    foreach(var item in model.list) 
                    {
                        total += item.borrow_fee;
                    }
                }

                ViewBag.total = total;
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }





    }	
}
