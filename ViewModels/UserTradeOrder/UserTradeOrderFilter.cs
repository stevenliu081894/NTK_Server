using NTKServer.Internal;

namespace NTKServer.ViewModels.UserTradeOrder
{
    public class UserTradeOrderFilter
    {
        /// <summary> sn: 委托单号 </summary>
        [Where("LIKE", "trade_order.sn")]
        public string? sn { get; set; }

        /// <summary> stock_code: 股票代码 </summary>
        [Where("=", "trade_order.stock_code")]
        public string? stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        [Where("LIKE", "trade_order.stock_name")]
        public string? stock_name { get; set; }

    }
}
