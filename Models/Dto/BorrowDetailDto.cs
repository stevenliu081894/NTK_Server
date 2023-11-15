using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class BorrowDetailDto
    {
        /// <summary> 配资记录FK </summary>
        public int borrow_fk { get; set; }
        /// <summary> 会员FK </summary>
        public int member_fk { get; set; }
        /// <summary> 主键PK </summary>
        public int pk { get; set; }
        /// <summary> 还息状态  0：待还  1：已还 </summary>
        public int status { get; set; }
        /// <summary> 利息 单位：元 </summary>
        public decimal interest { get; set; }
        /// <summary> 已还利息 </summary>
        public decimal receive_interest { get; set; }
        /// <summary> 当前批次 </summary>
        public int sort_order { get; set; }
        /// <summary> 总批次 </summary>
        public int total { get; set; }
        /// <summary> 到期时间 </summary>
        public int deadline { get; set; }
        /// <summary> 还款时间 </summary>
        public DateTime repayment_time { get; set; }
    }
}
