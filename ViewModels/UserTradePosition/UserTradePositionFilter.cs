using NTKServer.Internal;

namespace NTKServer.ViewModels.UserTradePosition
{
    public class UserTradePositionFilter
    {
        /// <summary> stock_code: 股票代码 </summary>
        [Where("=", "trade_position.stock_code")]
        public string? stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        [Where("LIKE", "trade_position.stock_name")]
        public string? stock_name { get; set; }

    }
}
