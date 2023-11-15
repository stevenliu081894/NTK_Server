using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.StatWalletRecordDetail;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class StatWalletRecordDetailController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(70, 11)]
		[TranslatorUIFilter("StatWalletRecordDetail")]
        public IActionResult Index(StatWalletRecordDetailFilter filter, int page = 1)
        {
            SetFilterSelect();

            StatWalletRecordDetailVm model = new()
            {
                filter = filter ?? new StatWalletRecordDetailFilter()
            };

            try
            {
                model.list = StatWalletRecordDetailBiz.GetStatWalletRecordDetailList(model.filter,GetLanguage()).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

        /// <summary>
        /// ��Աδ������ϸ
        /// </summary>
        [MenuFilter(71, 11)]
        public IActionResult Undone(StatWalletRecordDetailFilter filter, int page = 1)
        {
            SetFilterSelect();

            StatWalletRecordDetailVm model = new()
            {
                filter = filter ?? new StatWalletRecordDetailFilter()
            };

            try
            {
                model.list = StatWalletRecordDetailBiz.GetStatWalletRecordDetailList(model.filter, GetLanguage()).ToPagedList(page, pageSize);
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
