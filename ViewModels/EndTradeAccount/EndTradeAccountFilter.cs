using NTKServer.Internal;

namespace NTKServer.ViewModels.EndTradeAccount
{
    public class EndTradeAccountFilter
    {
		/// <summary> StartCloseTome: 結束時間起始 </summary>
		[Where(">=", "end_time")]
		public DateTime? StartCloseTome { get; set; }

		/// <summary> EndCloseTome: 結束時間結束 </summary>
		[Where("<", "end_time")]
		public DateTime? EndCloseTome { get; set; }

		/// <summary> sub_account: 子帐号 </summary>
		[Where("=", "sub_account")]
        public string? sub_account { get; set; }

        /// <summary> loan_type: 帳號類型 </summary>
        [Where("=", "borrow_plan.pk")]
        public string? loan_type { get; set; }

        /// <summary> market: 市场 </summary>
        [Where("=", "vw_trade_account.market")]
        public string? market { get; set; }

        /// <summary> member_fk: 客户編號 </summary>
        [Where("=", "member_fk")]
        public int? member_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "account")]
        public string? account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        [Where("LIKE", "member_name")]
        public string? member_name { get; set; }

    }
}
