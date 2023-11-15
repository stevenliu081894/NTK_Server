using NTKServer.Internal;

namespace NTKServer.ViewModels.ReviewMember
{
    public class ReviewMemberFilter
    {
        /// <summary> pk: 用户pk </summary>
        [Where("=", "pk")]
        public int? pk { get; set; }

        /// <summary> account: 登入帐号 </summary>
        [Where("=", "account")]
        public string? account { get; set; }

        /// <summary> nickname: 名称 </summary>
        [Where("LIKE", "nickname")]
        public string? nickname { get; set; }

        /// <summary> real_name: 真实姓名 </summary>
        [Where("LIKE", "real_name")]
        public string? real_name { get; set; }

        /// <summary> email: 电子信箱用于登陆 </summary>
        [Where("LIKE", "email")]
        public string? email { get; set; }

        /// <summary> create_ip: 注册IP </summary>
        [Where("=", "create_ip")]
        public string? create_ip { get; set; }

        /// <summary> auth_time: 实名认证申请时间 </summary>
        [Where("", "auth_time")]
        public DateTime? auth_time { get; set; }

    }
}
