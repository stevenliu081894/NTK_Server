using NTKServer.Internal;

namespace NTKServer.ViewModels.ExchangeRate
{
    public class ExchangeRateList
    {
        /// <summary> date: 日期 </summary>
        public string date { get; set; }

        /// <summary> currency_symbol: 币别A </summary>
        public string currency_symbol { get; set; }

        /// <summary> base_symbol: 币别B </summary>
        public string base_symbol { get; set; }

        /// <summary> inward_rate: A转B汇率 </summary>
        public decimal inward_rate { get; set; }

        /// <summary> outward_rate: B转A汇率 </summary>
        public decimal outward_rate { get; set; }

        /// <summary> create_time: 查询时间 </summary>
        public DateTime create_time { get; set; }

    }
}
