using NTKServer.Internal;

namespace NTKServer.ViewModels.StockHoliday
{
    public class StockHolidayFilter
    {
        /// <summary> market: 市场别 </summary>
        [Where("=", "stock_holiday.market")]
        public string? market { get; set; }

        /// <summary> name: 假期名稱 </summary>
        [Where("LIKE", "stock_holiday.name")]
        public string? name { get; set; }

        /// <summary> year: 年度 </summary>
        [Where("=", "stock_holiday.year")]
        public int? year { get; set; }

        /// <summary> date: 日期 </summary>
        [Where("=", "stock_holiday.date")]
        public string? date { get; set; }

    }
}
