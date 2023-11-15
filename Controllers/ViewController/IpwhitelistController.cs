using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.Ipwhitelist;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class IpwhitelistController : BaseController
    {
        private void SetFilterSelect()
        {
			
        }

        [MenuFilter(48, 10)]
        [TranslatorUIFilter("Ipwhitelist")]
        public IActionResult Index(IpwhitelistFilter filter, int page = 1)
        {
            SetFilterSelect();

            IpwhitelistVm model = new()
            {
                filter = filter ?? new IpwhitelistFilter()
            };

            try
            {
                model.list = IpwhitelistBiz.GetIpwhitelistList(model.filter).ToPagedList(page, pageSize);
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

        [UseFilter(48, 10)]
        [TranslatorUIFilter("Ipwhitelist")]		
		public IActionResult Edit(string ip)
        {
            SetSelect();
            AdminIpwhitelistDto dto = IpwhitelistBiz.Get(ip);
            return View(dto);
        }

        public IActionResult PostEdit(AdminIpwhitelistDto req)
        {
            SetSelect();
            try
            {
				IpwhitelistBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return RedirectToAction("Edit", req);
            }
        }

        [UseFilter(48, 10)]
        [TranslatorUIFilter("Ipwhitelist")]
        public IActionResult Create()
        {
            SetSelect();
            AdminIpwhitelistDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(AdminIpwhitelistDto req)
        {
            SetSelect();
            try
            {
				IpwhitelistBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

        [UseFilter(48, 10)]
        public IActionResult Delete(string ip)
        {
            try
            {
                IpwhitelistBiz.Delete(ip);
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
