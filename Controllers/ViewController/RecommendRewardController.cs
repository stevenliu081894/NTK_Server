using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.RecommendReward;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class RecommendRewardController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(11, 12)]
		[TranslatorUIFilter("RecommendReward")]
        public IActionResult Index(RecommendRewardFilter filter, int page = 1)
        {
            SetFilterSelect();

            RecommendRewardVm model = new()
            {
                filter = filter ?? new RecommendRewardFilter()
            };

            try
            {
                model.list = RecommendRewardBiz.GetRecommendRewardList(model.filter).ToPagedList(page, pageSize);
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
