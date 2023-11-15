using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Dto
{
    public class StockHolidayDto
    {
        /// <summary> 休市日PK </summary>
        public int pk { get; set; }
        /// <summary> 市场别 </summary>
        public string market { get; set; }
        /// <summary> 假期名稱 </summary>
        public string name { get; set; }
        /// <summary> 年度 </summary>
        public int year { get; set; }
		/// <summary> 日期 </summary>
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime date { get; set; }
		/// <summary> 是否全天休市 </summary>
		public bool is_allday { get; set; }

        /// <summary> 非全天休市的開盤時間UTC </summary>
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime? open { get; set; }
        /// <summary> 非全天休市的收盤時間UTC </summary>
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}")]
        public DateTime? close { get; set; }
    }
}
