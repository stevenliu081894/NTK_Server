using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MemberSignDto
    {
        /// <summary> 用户会员id </summary>
        public int member_fk { get; set; }
        /// <summary> id </summary>
        public int pk { get; set; }
        /// <summary> 签到时间戳 </summary>
        public DateTime sign_time { get; set; }
        /// <summary> 连续天数 </summary>
        public int continuity_day { get; set; }
        /// <summary> 总共签到天数 </summary>
        public int total_day { get; set; }
        /// <summary> 获得红包金额 </summary>
        public decimal coupon { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
    }
}
