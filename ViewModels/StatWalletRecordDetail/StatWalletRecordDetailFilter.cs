using NTKServer.Internal;

namespace NTKServer.ViewModels.StatWalletRecordDetail
{
    public class StatWalletRecordDetailFilter
    {
        /// <summary> admin_user_fk: 所属客服 </summary>
        [Where("=", "member.admin_user_fk")]
        public int? admin_user_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "member.account")]
        public string? account { get; set; }

        /// <summary> nickname: 名称 </summary>
        [Where("=", "member.nickname")]
        public string? nickname { get; set; }

    }
}
