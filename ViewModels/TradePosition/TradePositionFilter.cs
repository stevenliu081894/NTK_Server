using NTKServer.Internal;

namespace NTKServer.ViewModels.TradePosition
{
    public class TradePositionFilter
    {
        /// <summary> sub_account: 子帐号 </summary>
        [Where("=", "trade_position.sub_account")]
        public string? sub_account { get; set; }

        /// <summary> market: 市场 </summary>
        [Where("=", "trade_position.market")]
        public string? market { get; set; }

        /// <summary> stock_code: 股票代码 </summary>
        [Where("=", "trade_position.stock_code")]
        public string? stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        [Where("LIKE", "trade_position.stock_name")]
        public string? stock_name { get; set; }

        /// <summary> member_fk: 客户 PK </summary>
        [Where("=", "vw_trade_account.member_fk")]
        public int? member_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "vw_trade_account.account")]
        public string? account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        [Where("LIKE", "vw_trade_account.member_name")]
        public string? member_name { get; set; }

    }
}
