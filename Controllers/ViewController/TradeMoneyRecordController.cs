using DB.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.TradeMoneyRecord;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class TradeMoneyRecordController : BaseController
    {
        private void SetFilterSelect()
        {
            ViewBag.TradeTemp = TradeTemplateBiz.GetDropDownList(GetLanguage());
        }

		[UseFilter(391, 2)]
		[TranslatorUIFilter("TradeMoneyRecord")]
        public IActionResult Index(string subAccount, TradeMoneyRecordFilter filter, int page = 1)
        {
            TradeMoneyRecordVm model = new()
            {
                filter = filter ?? new TradeMoneyRecordFilter()
            };

            try
            {
                if (string.IsNullOrWhiteSpace(subAccount) && string.IsNullOrWhiteSpace(filter.sub_account))
                {
                    throw new AppException(210, "illegal_operation");
                }
                SetFilterSelect();
                model.filter.sub_account = subAccount ?? filter.sub_account;
                model.list = TradeMoneyRecordBiz.GetTradeMoneyRecordList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(391, 2)]
		[TranslatorUIFilter("TradeMoneyRecord")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            TradeMoneyRecordDto dto = TradeMoneyRecordBiz.Get(pk);
            return View(dto);
        }
    }	
}
