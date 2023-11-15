using NTKServer.Internal;

namespace NTKServer.ViewModels.Member
{
    public class MemberFilter
    {
        /// <summary> admin_user_fk: 服务人員 </summary>
        [Where("=", "member.admin_user_fk")]
        public int? admin_user_fk { get; set; }

        /// <summary> pk: 會員編號 </summary>
        [Where("=", "member.pk")]
        public int? pk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "member.account")]
        public string? account { get; set; }

        /// <summary> nickname: 名称 </summary>
        [Where("LIKE", "member.nickname")]
        public string? nickname { get; set; }

        /// <summary> real_name: 会员姓名 </summary>
        [Where("LIKE", "member.real_name")]
        public string? real_name { get; set; }

    }
}
