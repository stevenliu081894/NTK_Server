using Microsoft.AspNetCore.Mvc.Filters;

namespace NTKServer.Internal
{
    public class SessionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var path = context.Request.Path;

            //if (IsExclude(path) == false)
            //{
            //    string? token = context.Session.GetString("token");
            //    if (string.IsNullOrEmpty(token))
            //    {
            //        context.Response.Redirect("~/Login/Index");
            //    }
            //}

            await next(context);
        }

        private bool IsExclude(string path)
        {
            path = path.ToLower();
            List<string> exclude = new List<string>
            {
                "/",
                "/login/index",
                "/Login/SignIn",
                "/Login/ChangePasswd",
                "/Login/PostChangePasswd",
            };

            return exclude.Exists(t => t.ToLower() == path);
        }
    }
}
