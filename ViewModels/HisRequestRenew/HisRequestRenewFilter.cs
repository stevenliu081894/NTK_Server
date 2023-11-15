using NTKServer.Internal;

namespace NTKServer.ViewModels.HisRequestRenew
{
    public class HisRequestRenewFilter
    {
        /// <summary> begin_time: 申请起始时间 </summary>
        [Where(">=", "add_time")]
        public DateTime? begin_time { get; set; }

        /// <summary> end_time: 申请结束时间 </summary>
        [Where("<", "add_time")]
        public DateTime? end_time { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "vw_trade_account.account")]
        public string? account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        [Where("LIKE", "vw_trade_account.member_name")]
        public string? member_name { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        [Where("LIKE", "borrow_request.sub_account")]
        public string? sub_account { get; set; }
    }
}
