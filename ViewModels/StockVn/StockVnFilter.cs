using NTKServer.Internal;

namespace NTKServer.ViewModels.StockVn
{
    public class StockVnFilter
    {
        /// <summary> stock_code: 股票代码 </summary>
        [Where("LIKE", "stock_vn.stock_code")]
        public string? stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        [Where("LIKE", "stock_vn.stock_name")]
        public string? stock_name { get; set; }

    }
}
