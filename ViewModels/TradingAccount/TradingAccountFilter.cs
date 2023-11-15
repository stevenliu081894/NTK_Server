using NTKServer.Internal;

namespace NTKServer.ViewModels.TradingAccount
{
    public class TradingAccountFilter
    {
        /// <summary> market: 市场 </summary>
        [Where("=", "vw_trade_account.market")]
        public string? market { get; set; }

        /// <summary> sub_account: 子帐号 </summary>
        [Where("=", "sub_account")]
        public string? sub_account { get; set; }

        /// <summary> status: 状态 </summary>
        [Where("=", "status")]
        public int? status { get; set; }

        /// <summary> loan_type: 合约类型 </summary>
        [Where("=", "borrow_plan.pk")]
        public string? loan_type { get; set; }

        /// <summary> end_time: 结束时间 </summary>
        [Where("<=", "date(vw_trade_account.end_time)")]
        public DateTime? end_time { get; set; }

        /// <summary> account: 帐号 </summary>
        [Where("=", "account")]
        public string? account { get; set; }

        /// <summary> member_name: 姓名 </summary>
        [Where("LIKE", "member_name")]
        public string? member_name { get; set; }

    }
}
