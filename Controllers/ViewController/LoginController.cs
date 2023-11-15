using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Org.BouncyCastle.Ocsp;
using NTKServer.Business;
using NTKServer.Data.Enums;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Models;
using NTKServer.Tool;
using NTKServer.ViewModels;
using NTKServer.ViewModels.Login;

namespace NTKServer.Controllers
{
    public class LoginController : BaseController
    {
        public IActionResult Index()
        {
            DeleteSession("token");
            HttpContext.Session.Clear();

            ViewBag.Admins = LoginBiz.GetAdminListVm();
            LoginVm model = new();
            return View(model);
        }

        public IActionResult SignIn(LoginVm model)
        {
            var ip = PublicTool.Text(HttpContext.Connection.RemoteIpAddress);
			try
			{   
                // 1. 驗證 IP 允許登錄
                LoginBiz.CheckWhiteList(ip, model.LoginProvider);

				// 2. 驗證碼驗證
				string captchaCode = GetSession("Captcha.Code");
                // LoginBiz.AuthCaptcha(model.code, captchaCode);

                // 3. 帳密密碼驗證
                AdminSession user = LoginBiz.Authenticate(model.LoginProvider.ToString(), model.ProviderKey);

                // 4. 這裡不論成功與否, 紀錄 login 行為 
                AdminLoginBiz.PostCreate(new AdminLoginDto
                {
                    login_account = user.account,
                    ip = ip,
                    ip_country = PublicTool.GetCountryByIp(ip),
                    create_time = DateTime.UtcNow,
                    status = true,
                    remark = "录入成功"
				});

				// 5. 密碼變更後第一次登錄者, 強迫變更密碼

				//TODO: 警告, 上線後 false 要拿掉
				if (false && user.change_password == true)
				{
					ChangePasswdVm vm = new ChangePasswdVm();
                    vm.LoginProvider = model.LoginProvider;
					return RedirectToAction("ChangePasswd", vm );
                }

                // 6. 重新更新群組選單, 避免選單曾經有異動過
                AdminMenuBiz.UpdateMenu(user.role);

                // 7. 設置 Session
                SetSession("token", user);

                // 8. 跳轉到第一個AdminMenu，如果沒設定就跳轉到首頁

                var admin_role = AdminRoleBiz.Get(user.role);
                var admin_menu_dict = PublicTool.FromJson<Dictionary<int, int[]>>(admin_role.admin_menu);
                if (admin_menu_dict != null && 
                    admin_menu_dict.ContainsKey(admin_role.admin_module_fk) && 
                    admin_menu_dict[admin_role.admin_module_fk].Length > 0)
                    return Redirect(AdminMenuBiz.GetMenuByPK(admin_menu_dict[admin_role.admin_module_fk][0]).url_value);
                else
                    return RedirectToAction("Index", "home");
            }
            catch (AppException ex)
            {
                // 除了驗證碼錯誤, 都要紀錄 admin_login(包含 admin_login)
                if (ex.GetStatus() != 1200)
                {
					AdminLoginBiz.PostCreate(new AdminLoginDto
					{
						login_account = model.LoginProvider,
						ip = ip,
						ip_country = PublicTool.GetCountryByIp(ip),
						create_time = DateTime.UtcNow,
						status = false,
						remark = "录入失敗"
					});
				}

                ShowError(ex.Message);
                ViewBag.Admins = LoginBiz.GetAdminListVm();
                return View("Index", model);
            }
        }

        public IActionResult ChangePasswd(ChangePasswdVm req) 
        {
			return View(req);
		}

		public IActionResult PostChangePasswd(ChangePasswdVm req)
		{
			try
			{
                LoginBiz.ChangePassword(req.LoginProvider, req.ProviderKey, req.NewPassword, req.confirmPassword);
				return RedirectToAction("Index");
			}
			catch (AppException ex)
			{
				ShowWarning(ex.Message);
				return View("ChangePasswd", req);
			}
		}


		/// <summary>
		/// 沒有權限頁面
		/// </summary>
		public IActionResult UnAuthorized()
        {            
            return View();
        }
    }
}
