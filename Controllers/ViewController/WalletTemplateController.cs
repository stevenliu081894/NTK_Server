using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.WalletTemplate;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class WalletTemplateController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(30, 14)]
		[TranslatorUIFilter("WalletTemplate")]
        public IActionResult Index(WalletTemplateFilter filter, int page = 1)
        {
            SetFilterSelect();

            WalletTemplateVm model = new()
            {
                filter = filter ?? new WalletTemplateFilter()
            };

            try
            {
                model.list = WalletTemplateBiz.GetWalletTemplateList(model.filter).ToPagedList(page, pageSize);
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

		[UseFilter(30, 14)]
		[TranslatorUIFilter("WalletTemplate")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            WalletTemplateDto dto = WalletTemplateBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(WalletTemplateDto req)
        {
            SetSelect();
            try
            {
				WalletTemplateBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(30, 14)]
		[TranslatorUIFilter("WalletTemplate")]
        public IActionResult Create()
        {
            SetSelect();
            WalletTemplateDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(WalletTemplateDto req)
        {
            SetSelect();
            try
            {
				WalletTemplateBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }
    }	
}
