using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NTKServer.Business;
using NTKServer.Controllers;

namespace NTKServer.Filter
{
    /// <summary>
    /// 生成選單
    /// </summary>
    public class MenuFilter : ActionFilterAttribute
    {
        public int id { get; set; }
        public int mid { get; set; }
        private BaseController? controller;

        public MenuFilter(int menuId, int moduleId)
        {
            id = menuId;
            mid = moduleId;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                controller = filterContext.Controller as BaseController;
                var user = controller.GetUser();

                if (user == null || SetMenu(user.role, id, mid) == false)
                {
                    filterContext.Result = RedirectToLogin();
                }
            }
            catch (Exception)
            {
                filterContext.Result = RedirectToLogin();
            }
        }

        protected RedirectToRouteResult RedirectToLogin()
        {
            return new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Login" },
                    { "action", "Index" }
               });
        }

        public bool SetMenu(int roleId, int id, int model)
        {
            return false;
        }
    }
}
