using NTKServer.Internal;

namespace NTKServer.ViewModels.TradeMoneyCheck
{
    public class TradeMoneyCheckFilter
    {
        /// <summary> account: 会员帐号 </summary>
        [Where("=", "vw_trade_account.account")]
        public string? account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        [Where("=", "vw_trade_account.member_name")]
        public string? member_name { get; set; }

        /// <summary> sn: 申请单号 </summary>
        [Where("like", "trade_money_check.sn")]
        public string? sn { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        [Where("like", "trade_money_check.sub_account")]
        public string? sub_account { get; set; }

    }
}
