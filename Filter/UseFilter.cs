using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NTKServer.Business;
using NTKServer.Controllers;

namespace NTKServer.Filter
{
    /// <summary>
    /// 判斷是否有使用權限
    /// </summary>
    public class UseFilter : ActionFilterAttribute
    {
        public int id { get; set; }
        public int mid { get; set; }

        public UseFilter(int menuId, int moduleId)
        {
            id = menuId;
            mid = moduleId;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string? actionName = filterContext.HttpContext.Request.RouteValues["action"].ToString();
                string? controllerName = filterContext.HttpContext.Request.RouteValues["controller"].ToString();

                var controller = filterContext.Controller as BaseController;
                var user = controller.GetUser();

                if (CheckRolePower(user.role) == false)
                {
                    filterContext.Result = RedirectToUnAuthorized();
                }

                int parent = GetParentId(id);
                SetMenu(controller, user.role, parent, mid);

            }
            catch (Exception)
            {
                filterContext.Result = RedirectToUnAuthorized();
            }
        }

        /// <summary>
        /// 驗證角色權限
        /// </summary>
        public bool CheckRolePower(int role)
        {
            return AdminRoleBiz.VerifyPower(id, mid, role);
        }

        public int GetParentId(int id)
        {
            var data = AdminMenuBiz.GetMenuByPK(id);
            return data.parent;
        }

        protected RedirectToRouteResult RedirectToUnAuthorized()
        {
            return new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Login" },
                    { "action", "UnAuthorized" }
               });
        }

        public bool SetMenu(BaseController controller, int roleId, int id, int model)
        {
            var mainMenu = AdminMenuBiz.GetMenuByRole(roleId);

            foreach (var item in mainMenu)
            {
                if (item.Id == model)
                {
                    item.Active = "active";
                    if (item.Child != null)
                    {
                        foreach (var child in item.Child)
                        {
                            if (child.Id == id)
                            {
                                child.Css += " active ";
                                controller.ViewData["Menu"] = mainMenu;
                                controller.ViewData["workCtrl"] = item.Title;
                                controller.ViewData["workAction"] = child.Title;
                                controller.ViewData["Title"] = child.Title;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }    
}
