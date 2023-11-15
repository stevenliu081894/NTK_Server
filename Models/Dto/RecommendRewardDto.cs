using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RecommendRewardDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 會員pk </summary>
        public int member_fk { get; set; }
        /// <summary> 年 </summary>
        public string year { get; set; }
        /// <summary> 月 </summary>
        public string month { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
        /// <summary> 本月份分潤總額 </summary>
        public decimal total_reward { get; set; }
        /// <summary> 状态: -1_计算中 0_未提现 1_申请提现中 2_已提现  3_异常 </summary>
        public int state { get; set; }
        /// <summary> 用户申请提现日期 </summary>
        public DateTime? withdraw { get; set; }
        /// <summary> 实际发放佣金时间 </summary>
        public DateTime? paydate { get; set; }
        /// <summary> 备注 </summary>
        public string note { get; set; }
        /// <summary> 創建時間 </summary>
        public DateTime create_time { get; set; }
    }
}
