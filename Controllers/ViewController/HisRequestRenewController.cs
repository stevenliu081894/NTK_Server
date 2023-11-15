using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.HisRequestRenew;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class HisRequestRenewController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(358, 3)]
		[TranslatorUIFilter("HisRequestRenew")]
        public IActionResult Index(HisRequestRenewFilter filter, int page = 1)
        {
            SetFilterSelect();

            HisRequestRenewVm model = new()
            {
                filter = filter ?? new HisRequestRenewFilter()
            };

            try
            {
                model.list = HisRequestRenewBiz.GetHisRequestRenewList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(358, 3)]
		[TranslatorUIFilter("HisRequestRenew")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            BorrowRequestDto dto = HisRequestRenewBiz.Get(pk);
            return View(dto);
        }
    }	
}
