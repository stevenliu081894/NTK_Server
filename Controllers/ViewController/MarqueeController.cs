using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.Marquee;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class MarqueeController : BaseController
    {
        public void SetSelect()
        {
            ViewBag.langDropdown = MultiLangBiz.FindSelectList();
        }

        [MenuFilter(174, 7)]
		[TranslatorUIFilter("Marquee")]
        public IActionResult Index(MarqueeFilter filter, int page = 1)
        {
            SetSelect();
            MarqueeVm model = new()
            {
                filter = filter ?? new MarqueeFilter()
            };

            try
            {
                model.list = MarqueeBiz.GetMarqueeList(model.filter);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

		[UseFilter(174, 7)]
		[TranslatorUIFilter("Marquee")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            CmsMarqDto dto = MarqueeBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(CmsMarqDto req)
        {
            SetSelect();
            try
            {
				MarqueeBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(174, 7)]
		[TranslatorUIFilter("Marquee")]
        public IActionResult Create()
        {
            SetSelect();
            CmsMarqDto dto = new();
            dto.enable = true;
            return View(dto);
        }

        public IActionResult PostCreate(CmsMarqDto req)
        {
            SetSelect();
            try
            {
				MarqueeBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(174, 7)]
        public IActionResult Delete(int pk)
        {
            try
            {
                MarqueeBiz.Delete(pk);
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
