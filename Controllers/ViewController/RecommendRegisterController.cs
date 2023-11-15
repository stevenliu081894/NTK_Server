using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.RecommendRegister;
using X.PagedList;

namespace NTKServer.Controllers
{
	public class RecommendRegisterController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(10, 12)]
		[TranslatorUIFilter("RecommendRegister")]
        public IActionResult Index(RecommendRegisterFilter filter, int page = 1)
        {
            SetFilterSelect();
            RecommendRegisterVm model = new()
            {
                filter = filter ?? new RecommendRegisterFilter()
            };
            try
            {
                model.list = RecommendRegisterBiz.GetRecommendRegisterList(model.filter).ToPagedList(page, pageSize);
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

		[MenuFilter(10, 12)]
		[TranslatorUIFilter("RecommendRegister")]		
		public IActionResult Detail(int id)
        {
            SetSelect();
            RecommendRegisterList dto = RecommendRegisterBiz.GetDetailVm(id);
            return View(dto);
        }
    }	
}
