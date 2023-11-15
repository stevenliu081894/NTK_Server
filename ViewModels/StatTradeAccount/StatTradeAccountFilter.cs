using NTKServer.Internal;

namespace NTKServer.ViewModels.StatTradeAccount
{
    public class StatTradeAccountFilter
    {
        /// <summary> start_time: 起始年 </summary>
        public int? start_year { get; set; }

        /// <summary> start_month: 起始月份 </summary>
        public int? start_month { get; set; }

        /// <summary> end_year: 结束年份 </summary>
        public int? end_year { get; set; }

        /// <summary> end_month: 结束份月 </summary>
        public int? end_month { get; set; }
    }
}
