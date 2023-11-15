using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.AdminUser;
using NTKServer.ViewModels.ExchangeRate;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class ExchangeRateController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(423, 5)]
		[TranslatorUIFilter("ExchangeRate")]
        public IActionResult Index(ExchangeRateFilter Filter, int page = 1)
        {
            SetFilterSelect();
			ExchangeRateVm model = new()
			{
				filter = Filter ?? new ExchangeRateFilter()
			};
            try
            {
                model.list = ExchangeRateBiz.GetExchangeRateList(model.filter).ToPagedList(page, pageSize);
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
