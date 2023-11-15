using System.ComponentModel.DataAnnotations;

namespace NTKServer.ViewModels.StockHoliday
{
    public class StockHolidayList
    {
        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> market: 市场别 </summary>
        public string market { get; set; }

        /// <summary> name: 假期名稱 </summary>
        public string name { get; set; }

        /// <summary> year: 年度 </summary>
        public int year { get; set; }

		/// <summary> date: 日期 </summary>
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime date { get; set; }

        /// <summary> is_allday: 是否全天休市 </summary>
        public bool is_allday { get; set; }

        /// <summary> open: 本日开盘时间 </summary>
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime? open { get; set; }

        /// <summary> close: 本日收盘时间 </summary>
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime? close { get; set; }

    }
}
