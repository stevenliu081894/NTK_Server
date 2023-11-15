using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RichboxBookDto
    {
        /// <summary> 用户FK </summary>
        public int member_fk { get; set; }
        /// <summary> 总资产 </summary>
        public decimal total_assets { get; set; }
        /// <summary> 帐户利息 </summary>
        public decimal profit { get; set; }
        /// <summary> 每日收益 </summary>
        public decimal day_earning { get; set; }
        /// <summary> 累积收益 </summary>
        public decimal total_earing { get; set; }
        /// <summary> 最高资产纪录 </summary>
        public decimal max_assets { get; set; }
    }
}
