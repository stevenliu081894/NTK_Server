using NTKServer.Internal;

namespace NTKServer.ViewModels.AdminLogin
{
    public class AdminLoginFilter
    {
        /// <summary> begin_time: 起始登录时间 </summary>
        [Where(">=", ".begin_time")]
        public DateTime? begin_time { get; set; }

        /// <summary> end_time: 结束登录时间 </summary>
        [Where("<", ".end_time")]
        public DateTime? end_time { get; set; }

        /// <summary> login_account: 帐号 </summary>
        [Where("=", "admin_login.login_account")]
        public string? login_account { get; set; }

        /// <summary> ip: IP </summary>
        [Where("=", "admin_login.ip")]
        public string? ip { get; set; }

        /// <summary> ip_country: IP国家 </summary>
        [Where("=", "admin_login.ip_country")]
        public string? ip_country { get; set; }

    }
}
