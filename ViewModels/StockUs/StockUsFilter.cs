using NTKServer.Internal;

namespace NTKServer.ViewModels.StockUs
{
    public class StockUsFilter
    {
        /// <summary> stock_code: 股票代码 </summary>
        [Where("LIKE", "stock_us.stock_code")]
        public string? stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        [Where("LIKE", "stock_us.stock_name")]
        public string? stock_name { get; set; }

    }
}
