using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.WalletRecord;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class WalletRecordController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[UseFilter(266, 5)]
		[TranslatorUIFilter("WalletRecord")]
        public IActionResult Index(int id, int page = 1)
        {
            SetFilterSelect();

            try
            {
                ViewBag.User = id;

                IPagedList<WalletRecordList> model = WalletRecordBiz.GetWalletRecordList(id).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(new WalletRecordList());
            }
        }
    }	
}
