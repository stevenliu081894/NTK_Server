using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class HistoryDailyVnDto
    {
        /// <summary> 日線歷史資料PK </summary>
        public int pk { get; set; }
        /// <summary> 日期 </summary>
        public string date { get; set; }
        /// <summary> 股票名稱 </summary>
        public string stock_code { get; set; }
        /// <summary> 開盤價 </summary>
        public double open { get; set; }
        /// <summary> 最高價 </summary>
        public double high { get; set; }
        /// <summary> 最低價 </summary>
        public double low { get; set; }
        /// <summary> 收盤價 </summary>
        public double close { get; set; }
        /// <summary> 成交量 </summary>
        public int volume { get; set; }
    }
}
