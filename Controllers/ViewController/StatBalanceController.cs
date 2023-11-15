using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.StatBalance;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class StatBalanceController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(72, 11)]
		[TranslatorUIFilter("StatBalance")]
        public IActionResult Index(StatBalanceFilter filter, int page = 1)
        {
            SetFilterSelect();

            StatBalanceVm model = new()
            {
                filter = filter ?? new StatBalanceFilter(),
                summary = new StatBalanceSummary()
            };

            try
            {
                var list = StatBalanceBiz.GetStatBalanceList(model.filter);

                model.summary.withdraw_total = 0;
                model.summary.recharge_total = 0;


                foreach (var item in list) 
                {
                    if(item.type == 1)
                    {
                        model.summary.recharge_count++;
                        model.summary.recharge_total += item.recharge;
                    }
                    else if (item.type == 12)
                    {
                        model.summary.withdraw_count++;
                        model.summary.withdraw_total += item.withdraw;
                    }
                }

                model.list = list.ToPagedList(page, pageSize);
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
