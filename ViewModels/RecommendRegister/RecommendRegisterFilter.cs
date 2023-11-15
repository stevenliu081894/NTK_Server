using NTKServer.Internal;

namespace NTKServer.ViewModels.RecommendRegister
{
    public class RecommendRegisterFilter
    {
        /// <summary> year: 年 </summary>
        public int? year { get; set; }

        /// <summary> month: 月份 </summary>
        public int? month { get; set; }

        /// <summary> admin_user_fk: 所属客服 </summary>
        [Where("=", "member.admin_user_fk")]
        public int? admin_user_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "member.account")]
        public string? account { get; set; }

        /// <summary> nickname: 会员名称 </summary>
        [Where("LIKE", "member.nickname")]
        public string? nickname { get; set; }

        /// <summary> real_name: 真实姓名 </summary>
        [Where("LIKE", "member.real_name")]
        public string? real_name { get; set; }

        /// <summary> Inviteaccount: 邀请人帐号 </summary>
        [Where("=", "invite.account")]
        public string? Inviteaccount { get; set; }

        /// <summary> Invitenickname: 邀请人名称 </summary>
        [Where("＝", "invite.nickname")]
        public string? Invitenickname { get; set; }

    }
}
