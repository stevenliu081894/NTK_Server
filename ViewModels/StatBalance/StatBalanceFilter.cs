using NTKServer.Internal;

namespace NTKServer.ViewModels.StatBalance
{
    public class StatBalanceFilter
    {
        /// <summary> start_year: 起始年份 </summary>
        public int? start_year { get; set; }

        /// <summary> start_month: 起始月份 </summary>
        public int? start_month { get; set; }

        /// <summary> end_year: 结束年份 </summary>
        public int? end_year { get; set; }

        /// <summary> end_month: 结束月份 </summary>
        public int? end_month { get; set; }

        /// <summary> : 资金类型 </summary>
        [Where("=", "t.type")]
        public int? type { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "t.account")]
        public string? account { get; set; }

        /// <summary> nickname: 会员名称 </summary>
        [Where("=", "t.nickname")]
        public string? nickname { get; set; }

    }
}
