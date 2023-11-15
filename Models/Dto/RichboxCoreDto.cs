using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RichboxCoreDto
    {
        /// <summary> 用户FK </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 资金存入日期 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 投入金额 </summary>
        public decimal invest { get; set; }
        /// <summary> 剩余金额 </summary>
        public decimal money { get; set; }
        /// <summary> 已投入天数 </summary>
        public int days { get; set; }
        /// <summary> 累积利息 </summary>
        public decimal pay { get; set; }
    }
}
