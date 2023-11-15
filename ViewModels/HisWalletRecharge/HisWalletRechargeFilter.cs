using NTKServer.Internal;

namespace NTKServer.ViewModels.HisWalletRecharge
{
    public class HisWalletRechargeFilter
    {
        /// <summary> status: 审核结果 </summary>
        [Where("=", "wallet_recharge.status")]
        public int? status { get; set; }

        /// <summary> verify_admin_pk: 審核人員 </summary>
        [Where("=", "wallet_recharge.verify_admin_pk")]
        public int? verify_admin_pk { get; set; }

        /// <summary> verify_time: 審核時間 </summary>
        [Where(">=", "date(wallet_recharge.verify_time)")]
        public DateTime? begin_verify_time { get; set; }

        /// <summary> verify_time: 審核時間 </summary>
        [Where("<", "date(wallet_recharge.verify_time)")]
        public DateTime? end_verify_time { get; set; }

        /// <summary> order_no: 充值單號 </summary>
        [Where("=", "wallet_recharge.order_no")]
        public string? order_no { get; set; }

        /// <summary> create_time: 充值申请时间 </summary>
        [Where(">=", "date(wallet_recharge.create_time)")]
        public DateTime? begin_create_time { get; set; }

        /// <summary> create_time: 充值申请时间 </summary>
        [Where("<", "date(wallet_recharge.create_time)")]
        public DateTime? end_create_time { get; set; }

        /// <summary> card: 收款卡号 </summary>
        [Where("=", "admin_bank.card")]
        public string? card { get; set; }

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
