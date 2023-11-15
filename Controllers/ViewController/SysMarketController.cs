using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.SysMarket;
using System.Collections.Generic;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class SysMarketController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

		[MenuFilter(227, 9)]
		[TranslatorUIFilter("SysMarket")]
        public IActionResult Index()
        {
            SetFilterSelect();

            try
            {
                List<SysMarketList> model = SysMarketBiz.GetSysMarketList();
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(null);
            }
        }

        public void SetSelect()
        {
		}

		[UseFilter(227, 9)]
		[TranslatorUIFilter("SysMarket")]		
		public IActionResult Edit(string code)
        {
            SetSelect();
            SysMarketDto dto = SysMarketBiz.Get(code);
            return View(dto);
        }

        public IActionResult PostEdit(SysMarketDto req)
        {
            SetSelect();
            try
            {
				SysMarketBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(227, 9)]
		[TranslatorUIFilter("SysMarket")]
        public IActionResult Create()
        {
            SetSelect();
            SysMarketDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(SysMarketDto req)
        {
            SetSelect();
            try
            {
				SysMarketBiz.PostCreate(req);
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
