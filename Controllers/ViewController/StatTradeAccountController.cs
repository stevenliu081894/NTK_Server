using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.StatTradeAccount;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class StatTradeAccountController : BaseController
    {
        private void SetFilterSelect()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem() { Text = "一月", Value = "1" },
                new SelectListItem() { Text = "二月", Value = "2" },
                new SelectListItem() { Text = "三月", Value = "3" },
                new SelectListItem() { Text = "四月", Value = "4" },
                new SelectListItem() { Text = "五月", Value = "5" },
                new SelectListItem() { Text = "六月", Value = "6" },
                new SelectListItem() { Text = "七月", Value = "7" },
                new SelectListItem() { Text = "八月", Value = "8" },
                new SelectListItem() { Text = "九月", Value = "9" },
                new SelectListItem() { Text = "十月", Value = "10" },
                new SelectListItem() { Text = "十一月", Value = "11" },
                new SelectListItem() { Text = "十二月", Value = "12" }
            };
			ViewBag.months = list;
        }

		[MenuFilter(316, 11)]
		[TranslatorUIFilter("StatTradeAccount")]
        public IActionResult Index(StatTradeAccountFilter filter, int page = 1)
        {
            SetFilterSelect();

            StatTradeAccountVm model = new()
            {
                filter = filter ?? new StatTradeAccountFilter()
            };

            try
            {
                model.list = StatTradeAccountBiz.GetStatTradeAccountList(model.filter).ToPagedList(page, pageSize);
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
