using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.ViewModels.AdminUser;
using X.PagedList;

namespace NTKServer.Controllers
{
    public class AdminUserController : BaseController
    {
        public void SetSelect()
        {
            ViewBag.roleDropdown = AdminRoleBiz.FindSelectList();
        }

		[MenuFilter(402, 10)]
		[TranslatorUIFilter("AdminUser")]
        public IActionResult Index(AdminUserFilter filter, int page = 1)
        {
            SetSelect();
            AdminUserVm model = new()
            {
                filter = filter ?? new AdminUserFilter()
            };

            try
            {
                model.list = AdminUserBiz.GetAdminUserList(model.filter).ToPagedList(page, pageSize);
                model.roleDropdown = AdminRoleBiz.FindSelectList();

				return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View(model);
            }
        }

        [UseFilter(402, 10)]
		[TranslatorUIFilter("AdminUser")]		
		public IActionResult Edit(int pk)
        {
            SetSelect();
            AdminUserDto dto = AdminUserBiz.Get(pk);
            return View(dto);
        }

        public IActionResult PostEdit(AdminUserDto req)
        {
            SetSelect();
            try
            {
				AdminUserBiz.PostEdit(req);               
                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {                
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

		[UseFilter(402, 10)]
		[TranslatorUIFilter("AdminUser")]
        public IActionResult Create()
        {
            SetSelect();
            AdminUserDto dto = new();
            return View(dto);
        }

        public IActionResult PostCreate(AdminUserDto req)
        {
            SetSelect();
            try
            {
                AdminUserBiz.PostCreate(req);
				return RedirectToAction("Index");	
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Create", req);
            }
        }

		[UseFilter(402, 10)]
        public IActionResult Delete(int pk)
        {
            SetSelect();
            try
            {
                AdminUserBiz.Delete(pk);
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
