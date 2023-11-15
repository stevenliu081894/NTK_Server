using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RichboxDailyReportDto
    {
        /// <summary> 日期 </summary>
        public string report_date { get; set; }
        /// <summary> 总投资额 </summary>
        public decimal total_invest { get; set; }
        /// <summary> 总产生利息 </summary>
        public decimal total_profit { get; set; }
        /// <summary> 今日投入金额 </summary>
        public decimal total_input { get; set; }
        /// <summary> 今日提出金额 </summary>
        public decimal total_output { get; set; }
        /// <summary> 今日取利息金额 </summary>
        public decimal total_interest { get; set; }
    }
}
