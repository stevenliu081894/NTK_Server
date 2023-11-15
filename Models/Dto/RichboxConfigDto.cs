using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RichboxConfigDto
    {
        /// <summary> 代码 </summary>
        public int id { get; set; }
        /// <summary> 是否启用 </summary>
        public bool enable { get; set; }
        /// <summary> 启用日期 </summary>
        public string active_date { get; set; }
        /// <summary> 停用日期 </summary>
        public string diactive_date { get; set; }
        /// <summary> 交易币别 </summary>
        public string currency { get; set; }
        /// <summary> 最低投资额 </summary>
        public decimal min_investment { get; set; }
        /// <summary> 最高投资额 </summary>
        public decimal max_investment { get; set; }
        /// <summary> 年利率 </summary>
        public decimal interest_rate { get; set; }
        /// <summary> 帐户起算利息金额 </summary>
        public decimal begin_profit { get; set; }
        /// <summary> 每日换日时间 </summary>
        public string closing_time { get; set; }
        /// <summary> 每日计算利息时间 </summary>
        public string give_interest_time { get; set; }
        /// <summary> 产品特点 </summary>
        public string feature { get; set; }
        /// <summary> 产品介绍 </summary>
        public string description { get; set; }
        /// <summary> 交易规则 </summary>
        public string trade_info { get; set; }
    }
}
