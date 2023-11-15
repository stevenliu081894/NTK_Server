using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RecommendRewardDetailDto
    {
        /// <summary> 推广月报pk(计算完成再填) </summary>
        public int recommend_reward_fk { get; set; }
        /// <summary> 被分钱會員的pk </summary>
        public int member_fk { get; set; }
        /// <summary> 收錢的會員pk </summary>
        public int parent { get; set; }
        /// <summary> 被分钱管理費紀錄pk </summary>
        public int borrow_fee_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 交易日期 </summary>
        public DateTime borrow_date { get; set; }
        /// <summary> 交易幣別 </summary>
        public string currency { get; set; }
        /// <summary> 管理費金額 </summary>
        public int management_fee { get; set; }
        /// <summary> 往上第幾代 </summary>
        public int generation { get; set; }
        /// <summary> 分潤%數 </summary>
        public decimal rate { get; set; }
        /// <summary> 分潤金额 </summary>
        public decimal reward { get; set; }
        /// <summary> 用戶名稱 </summary>
        public string nickname { get; set; }
        /// <summary> 上層用戶名稱 </summary>
        public string parent_nickname { get; set; }
    }
}
