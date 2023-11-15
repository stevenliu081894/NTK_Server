using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RecommendRewardSummaryDto
    {
        /// <summary> 會員pk </summary>
        public int member_fk { get; set; }
        /// <summary> 推广月报pk </summary>
        public int recommend_reward_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 第幾代 </summary>
        public int layer { get; set; }
        /// <summary> 年 </summary>
        public string year { get; set; }
        /// <summary> 月 </summary>
        public string month { get; set; }
        /// <summary> 合計人數 </summary>
        public int monthly_members { get; set; }
        /// <summary> 管理費總額 </summary>
        public decimal monthly_borrow_fee { get; set; }
        /// <summary> 獲取分潤 </summary>
        public decimal monthly_reward { get; set; }
    }
}
