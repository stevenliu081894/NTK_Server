using DB.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.Internal;
using NTKServer.Models.Tree;
using NTKServer.Tool;
using NTKServer.ViewModels.AdminRole;

namespace NTKServer.Controllers
{
    /// <summary>
    /// 角色權限管理
    /// </summary>
    public class AdminRoleController : BaseController
    {
        [MenuFilter(408, 10)]
        public IActionResult Index()
        {
            try
            {
                List<AdminRoleList> list = AdminRoleBiz.GetAdminRoleList();
                return View(list);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View();
            }
        }

        [UseFilter(411, 10)]
        public IActionResult Edit(int id)
        {
            try
            {
                AdminRoleDto result = AdminRoleService.Find(id);
                var tree = AdminRoleBiz.AdminMenuToTree(result.admin_menu);
                ViewBag.tree = PublicTool.ToJson(tree);
                ViewBag.admin_module_list = AdminModuleBiz.GetEnabledList();

                return View(result);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View();
            }            
        }

        public IActionResult PostEdit(AdminRoleDto req, string tree) 
        {
            try
            {
                req.admin_menu = AdminRoleBiz.TreeToAdminMenu(tree);
                AdminRoleBiz.PostEdit(req);

                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Edit", req);
            }
        }

        [UseFilter(411, 10)]
        public IActionResult Create(AdminRoleDto? req)
        {
            try
            {
                AdminRoleDto model = req ?? new();
                var tree = AdminRoleBiz.AdminMenuToTree(string.Empty);
                ViewBag.tree = PublicTool.ToJson(tree);
                return View(model);
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return View("Index");
            }
        }

        public IActionResult PostCreate(AdminRoleDto req, string tree)
        {
            try
            {
                req.admin_menu = AdminRoleBiz.TreeToAdminMenu(tree);
                AdminRoleBiz.PostCreate(req);

                return RedirectToAction("Index");
            }
            catch (AppException ex)
            {
                ShowWarning(ex.Message);
                return RedirectToAction("Create", req);
            }
        }
    }
}
