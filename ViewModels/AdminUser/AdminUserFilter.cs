using NTKServer.Internal;

namespace NTKServer.ViewModels.AdminUser
{
    public class AdminUserFilter
    {
        /// <summary> account: 用户名 </summary>
        [Where("=", "account")]
        public string? account { get; set; }

        /// <summary> role: 用戶角色 </summary>
        [Where("=", "role")]
        public int? role { get; set; }

        /// <summary> nickname: 昵称 </summary>
        [Where("LIKE", "nickname")]
        public string? nickname { get; set; }

    }
}
