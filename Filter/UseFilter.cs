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
            return true;
        }

        public int GetParentId(int id)
        {
            return 1;
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
            return false;
        }
    }    
}
