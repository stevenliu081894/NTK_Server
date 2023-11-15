using NTKServer.Internal;

namespace NTKServer.ViewModels.WalletWithdraw
{
    public class WalletWithdrawFilter
    {
        /// <summary> order_no: 申请编号 </summary>
        [Where("LIKE", "wallet_withdraw.order_no")]
        public string? order_no { get; set; }

        /// <summary> card: 收款卡號 </summary>
        [Where("LIKE", "member_bank.card")]
        public string? card { get; set; }

        /// <summary> bank_account: 卡号持有人 </summary>
        [Where("", "member_bank.bank_account")]
        public string? bank_account { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "member.account")]
        public string? account { get; set; }

        /// <summary> nickname: 会员名称 </summary>
        [Where("LIKE", "member.nickname")]
        public string? nickname { get; set; }

    }
}
