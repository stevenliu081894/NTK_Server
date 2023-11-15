using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.HisBorrow;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class HisBorrowController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(324, 3)]
		[TranslatorUIFilter("HisBorrow")]
        public IActionResult Index(HisBorrowFilter filter, int page = 1)
        {
            SetFilterSelect();

            HisBorrowVm model = new()
            {
                filter = filter ?? new HisBorrowFilter()
            };

            try
            {
                model.list = HisBorrowBiz.GetHisBorrowList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(324, 3)]
		[TranslatorUIFilter("HisBorrow")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
			HisBorrowEditVm dto = HisBorrowBiz.GetDetailVm(pk);
            return View(dto);
		}
    }	
}
