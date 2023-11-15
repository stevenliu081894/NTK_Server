using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RichboxRecordDto
    {
        /// <summary> 用户FK </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 影响金额 </summary>
        public decimal affect { get; set; }
        /// <summary> 账户余额 </summary>
        public decimal account { get; set; }
        /// <summary> 资金类型 1.存入 2.取出 3.利息 </summary>
        public int type { get; set; }
        /// <summary> 详情 </summary>
        public string info { get; set; }
        /// <summary> 交易时间 </summary>
        public DateTime create_time { get; set; }
    }
}
