using NTKServer.Internal;

namespace NTKServer.ViewModels.UserTradeDeal
{
    public class UserTradeDealFilter
    {
        /// <summary> deal_id: 成交单号 </summary>
        [Where("=", "trade_deal.deal_id")]
        public string? deal_id { get; set; }

        /// <summary> trade_order_sn: 原委托单号 </summary>
        [Where("=", "trade_deal.trade_order_sn")]
        public string? trade_order_sn { get; set; }

        /// <summary> stock_code: 股票代码 </summary>
        [Where("=", "trade_deal.stock_code")]
        public string? stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        [Where("LIKE", "trade_deal.stock_name")]
        public string? stock_name { get; set; }

    }
}
