using Microsoft.AspNetCore.Mvc.Rendering;
using NTKServer.Internal;
using NTKServer.Models;
using Models.Dto;
using DB.Services;
using NTKServer.Libs;

namespace NTKServer.Business
{
    public class LoginBiz
    {
        private static bool VerifyPassword(string password, string dbpassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, dbpassword);
        }

        public static void CheckWhiteList(string ip, string account)
        {
            var enable_white_list = Convert.ToInt32(ConfigLib.Get("enable_white_list"));
            if (enable_white_list == 1)
            {
                var white_list = AdminIpwhitelistService.FindByIpAndAccount(ip, account);
                if (white_list == null || white_list.status == 0)
                {
                    throw new AppException(300, "ip_reject");
                };
            }
        }

        public static AdminSession Authenticate(string account, string password)
        {
            AdminUserDto admin = AdminUserBiz.GetByAccount(account);
            if(admin == null )
            {
                throw new AppException(1270, "none_account");
            }

            if(admin.status == 0)
            {
                throw new AppException(1230, "account_expired");
            }

            //if (VerifyPassword(password, admin.password) == false)
            //{
            //    throw new AppException(1209, "incorrect_password");
            //}

            AdminSession session = new AdminSession
            {
                pk = admin.pk,
                role = admin.role,
                account = admin.account,
                nickName = admin.nickname,
                avatar = admin.avatar,
                lang = admin.lang,
                change_password = admin.change_password
            };

            return session;            
    }

        /// <summary>
        /// 驗證圖形驗證碼
        /// </summary>
        public static void AuthCaptcha(string userCode, string sysCode)
        {
            if (string.IsNullOrEmpty(sysCode))
            {
                throw new AppException(1200, "incorrect_captcha_code");
            }

            if(userCode != sysCode)
            {
                throw new AppException(1200, "incorrect_captcha_code");
            }
        }

        /// <summary>
        /// 開發階段用途
        /// </summary>
        public static List<SelectListItem> GetAdminListVm()
        {
            List<SelectListItem> items = new();
            var admins = AdminUserService.FindAdminList();
            foreach (var admin in admins)
            {
                items.Add(new SelectListItem { Text = admin.nickname, Value = admin.account });
            }
            
            return items;
        }

		internal static void ChangePassword(string loginProvider, string providerKey, string newPassword, string confirmPassword)
		{          		
			if (newPassword != confirmPassword)
			{
                throw new AppException(1320, "new_password_inconsistent");
			}
			    
			if (providerKey == newPassword)
            {
                throw new AppException(1219, "same_new_current_password");
			}

            if(newPassword.Length <= 5)
            {
				throw new AppException(1208, "password_length_6_96_error");				    
			}

            // 验证原始密码
            Authenticate(loginProvider, providerKey);
            string newpasswd = BCrypt.Net.BCrypt.HashPassword(newPassword);
            AdminUserService.UpdatePassword(loginProvider, newpasswd, false);
		}
	}
}
