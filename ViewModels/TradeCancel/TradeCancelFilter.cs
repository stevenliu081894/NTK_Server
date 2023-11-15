using NTKServer.Internal;

namespace NTKServer.ViewModels.TradeCancel
{
    public class TradeCancelFilter
    {
        /// <summary> trade_order_sn: 原始委托单 </summary>
        [Where("LIKE", "trade_cancel.trade_order_sn")]
        public string? trade_order_sn { get; set; }

        /// <summary> stock_code: 股票代码 </summary>
        [Where("=", "trade_cancel.stock_code")]
        public string? stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        [Where("LIKE", "trade_cancel.stock_name")]
        public string? stock_name { get; set; }

    }
}
