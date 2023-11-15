using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.Banner;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class BannerController : BaseController
    {
		public void SetSelect()
		{
			ViewBag.langDropdown = MultiLangBiz.FindSelectList();
		}

		[MenuFilter(220, 7)]
		[TranslatorUIFilter("Banner")]
        public IActionResult Index(BannerFilter filter, int page = 1)
        {
			SetSelect();
            BannerVm model = new()
            {
                filter = filter ?? new BannerFilter()
            };

            try
            {
                model.list = BannerBiz.GetBannerList(model.filter).ToPagedList(page, pageSize);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(220, 7)]
		[TranslatorUIFilter("Banner")]		
		public IActionResult Edit(int cms_files_fk)
        {
            SetSelect();
            CmsBannerDto dto = BannerBiz.Get(cms_files_fk);
            return View(dto);
        }

        public IActionResult PostEdit(CmsBannerDto req)
        {
            SetSelect();
            try
            {
				BannerBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(220, 7)]
		[TranslatorUIFilter("Banner")]
        public IActionResult Create()
        {
            SetSelect();
            CmsBannerDto dto = new();
            dto.sizes = new List<SelectListItem>();
            dto.sizes.Add(new SelectListItem() { Text = "PC", Value = "0", Selected = false });
            dto.sizes.Add(new SelectListItem() { Text = "手機", Value = "1", Selected = false });

            return View(dto);
        }

        public IActionResult PostCreate(CmsBannerDto req)
        {
            SetSelect();
            try
            {
				BannerBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowError(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(220, 7)]
        public IActionResult Delete(int cms_files_fk)
        {
            try
            {
                BannerBiz.Delete(cms_files_fk);
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return RedirectToAction("Index");
            }
        }


    }	
}
