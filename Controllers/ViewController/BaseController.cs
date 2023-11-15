using Microsoft.AspNetCore.Mvc;
using NTKServer.Models;
using NTKServer.Tool;
using NTKServer.ViewModels;

namespace NTKServer.Controllers
{
    public class BaseController : Controller
    {
        protected int pageSize = 20;

        #region 錯誤訊息視窗
        /// <summary>
        /// 秀錯誤
        /// </summary>
        protected void ShowError(string message)
        {
            SetMessage(message, MessageLevelEnum.Error);
        }

        /// <summary>
        /// 秀警告
        /// </summary>
        protected void ShowWarning(string message)
        {
            SetMessage(message, MessageLevelEnum.warning);
        }

        protected void ShowMessage(string message)
        {
            SetMessage(message, MessageLevelEnum.Info);
        }

        protected void SetMessage(string message, MessageLevelEnum level)
        {
            ErrorVm vm = new()
            {
                Message = message,               
            };

            switch(level)
            {
                case MessageLevelEnum.Info:
                    vm.Css = "alert-info";
                    break;
                case MessageLevelEnum.warning:
                    vm.Css = "alert-warning";
                    break;
                case MessageLevelEnum.Error:
                    vm.Css = "alert-danger";
                    break;
                default:
                    vm.Css = "alert-success";
                    break;
            }

            ViewBag.ShowAlter = vm;
        }
        #endregion

        #region session 管理

        /// <summary>
        /// 取得當前管理員
        /// </summary>
        public AdminSession GetUser()
        {
            try
            {
                string session = GetSession("token");

                if (string.IsNullOrEmpty(session))
                {
                    HttpContext.Response.Redirect("~/Login/Index");
                }

                AdminSession admin = PublicTool.FromJson<AdminSession>(session);
                return admin;
            }
            catch (Exception)
            {
                HttpContext.Response.Redirect("~/Login/Index");
                return null;
            }
        }

        /// <summary>
        /// 取得管理員的語系
        /// </summary>
        public string GetLanguage()
        {  
            var user = GetUser();
            if(user == null) return string.Empty;
            return user.lang;
        }

        /// <summary>
        /// 設置 session
        /// </summary>
        protected string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        protected void SetSession(string key, object value)
        {
            string jsonString = PublicTool.ToJson(value);
            HttpContext.Session.SetString( key, jsonString);
        }

        /// <summary>
        /// 刪除 session
        /// </summary>
        protected void DeleteSession(string key)
        {
            HttpContext.Session.Remove(key);
        }

        #endregion
    }
}
