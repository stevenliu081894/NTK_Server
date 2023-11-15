using NTKServer.Internal;

namespace NTKServer.ViewModels.EndWalletWithdraw
{
    public class EndWalletWithdrawFilter
    {

        [Where(">=", "date(wallet_withdraw.verify_time)")]
        public DateTime? begin_verify_time { get; set; }

        /// <summary> verify_time: 審核時間 </summary>
        [Where("<", "date(wallet_withdraw.verify_time)")]
        public DateTime? end_verify_time { get; set; }

        /// <summary> create_time: 充值申请时间 </summary>
        [Where(">=", "date(wallet_withdraw.create_time)")]
        public DateTime? begin_create_time { get; set; }

        /// <summary> create_time: 充值申请时间 </summary>
        [Where("<", "date(wallet_withdraw.create_time)")]
        public DateTime? end_create_time { get; set; }

        /// <summary> verify_admin_pk: 審核人員 </summary>
        [Where("=", "wallet_withdraw.verify_admin_pk")]
        public int? verify_admin_pk { get; set; }

        /// <summary> order_no: 申请编号 </summary>
        [Where("LIKE", "wallet_withdraw.order_no")]
        public string? order_no { get; set; }

        /// <summary> card: 收款卡號 </summary>
        [Where("LIKE", "member_bank.card")]
        public string? card { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "member.account")]
        public string? account { get; set; }

        /// <summary> nickname: 会员名称 </summary>
        [Where("LIKE", "member.nickname")]
        public string? nickname { get; set; }

    }
}
