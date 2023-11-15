using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.RecommendReward;
using NTKServer.ViewModels.RewardDetail;
using System.Collections.Generic;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class RewardDetailController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[UseFilter(12, 12)]
		[TranslatorUIFilter("RewardDetail")]
        public IActionResult Index(int id, RewardDetailFilter filter, int page = 1)
        {
            SetFilterSelect();
            RewardDetailVm model = new()
            {
                filter = filter ?? new RewardDetailFilter()
            };

            try
            {
                ViewBag.id = id;
                model.list = RewardDetailBiz.GetRewardDetailList(id).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(null);
            }
        }
    }	
}
