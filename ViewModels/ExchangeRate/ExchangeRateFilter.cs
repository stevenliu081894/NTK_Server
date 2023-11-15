using NTKServer.Internal;

namespace NTKServer.ViewModels.ExchangeRate
{
    public class ExchangeRateFilter
    {
        /// <summary> begin_time: 开始日期 </summary>
        [Where(">=", "date(view_exchange_rate.create_time)")]
        public DateTime? begin_time { get; set; }

        /// <summary> end_time: 结束日期 </summary>
        [Where("<", "date(view_exchange_rate.create_time)")]
        public DateTime? end_time { get; set; }

        /// <summary> currency_symbol: 币别A </summary>
        [Where("=", "currency_symbol")]
        public string? currency_symbol { get; set; }

        /// <summary> base_symbol: 币别B </summary>
        [Where("=", "base_symbol")]
        public string? base_symbol { get; set; }

    }
}
