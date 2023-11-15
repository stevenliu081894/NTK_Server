using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.HisAddfinancing;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class HisAddfinancingController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(327, 3)]
		[TranslatorUIFilter("HisAddfinancing")]
        public IActionResult Index(HisAddfinancingFilter filter, int page = 1)
        {
            SetFilterSelect();

            HisAddfinancingVm model = new()
            {
                filter = filter ?? new HisAddfinancingFilter()
            };

            try
            {
                model.list = HisAddfinancingBiz.GetHisAddfinancingList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(327, 3)]
		[TranslatorUIFilter("HisAddfinancing")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
			HisAddfinancingEditVm dto = HisAddfinancingBiz.GetEditVm(pk);
            return View(dto);
        }
    }	
}
