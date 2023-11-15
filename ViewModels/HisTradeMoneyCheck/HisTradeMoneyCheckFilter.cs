using NTKServer.Internal;

namespace NTKServer.ViewModels.HisTradeMoneyCheck
{
    public class HisTradeMoneyCheckFilter
    {
        /// <summary> start_time: 起始提盈时间 </summary>
        [Where(">=", ".start_time")]
        public DateTime? start_time { get; set; }

        /// <summary> end_time: 结束提盈时间 </summary>
        [Where("<", ".end_time")]
        public DateTime? end_time { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "vw_trade_account.account")]
        public string? account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        [Where("=", "vw_trade_account.member_name")]
        public string? member_name { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        [Where("like", "trade_money_check.sub_account")]
        public string? sub_account { get; set; }

    }
}
