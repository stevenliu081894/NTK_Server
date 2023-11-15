using NTKServer.Internal;

namespace NTKServer.ViewModels.Wallet
{
    public class WalletFilter
    {
        /// <summary> member_fk: 用户编号 </summary>
        [Where("=", "member_fk")]
        public int? member_fk { get; set; }

        /// <summary> account: 登入帐号 </summary>
        [Where("=", "account")]
        public string? account { get; set; }

        /// <summary> nickname: 名称 </summary>
        [Where("LIKE", "nickname")]
        public string? nickname { get; set; }

    }
}
