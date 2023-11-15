using NTKServer.Internal;

namespace NTKServer.ViewModels.WalletRecharge
{
    public class WalletRechargeFilter
    {
        /// <summary> card: 收款卡号 </summary>
        [Where("=", "admin_bank.card")]
        public string? card { get; set; }

        /// <summary> bank_name: 收款银行 </summary>
        [Where("LIKE", "admin_bank.bank_name")]
        public string? bank_name { get; set; }

        /// <summary> order_no: 充值單號 </summary>
        [Where("=", "wallet_recharge.order_no")]
        public string? order_no { get; set; }

        /// <summary> line_bank: 充值信息 </summary>
        [Where("LIKE", "wallet_recharge.line_bank")]
        public string? line_bank { get; set; }

        /// <summary> type: 充值方式 </summary>
        [Where("=", "wallet_recharge.type")]
        public string? type { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "member.account")]
        public string? account { get; set; }

        /// <summary> nickname: 名称 </summary>
        [Where("LIKE", "member.nickname")]
        public string? nickname { get; set; }

    }
}
