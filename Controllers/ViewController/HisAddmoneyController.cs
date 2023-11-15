using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.HisAddmoney;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class HisAddmoneyController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(332, 3)]
		[TranslatorUIFilter("HisAddmoney")]
        public IActionResult Index(HisAddmoneyFilter filter, int page = 1)
        {
            SetFilterSelect();

            HisAddmoneyVm model = new()
            {
                filter = filter ?? new HisAddmoneyFilter()
            };

            try
            {
                model.list = HisAddmoneyBiz.GetHisAddmoneyList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(332, 3)]
		[TranslatorUIFilter("HisAddmoney")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            HisAddmoneyEditVm dto = HisAddmoneyBiz.GetEditVm(pk);
            return View(dto);
        }
    }	
}
