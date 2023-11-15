using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletFreezeDto
    {
        /// <summary> 钱包id </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 序号.由冻结方提供 </summary>
        public string sn { get; set; }
        /// <summary> 冻结金额 </summary>
        public decimal freeze { get; set; }
        /// <summary>  </summary>
        public int subtype { get; set; }
        /// <summary> 冻结时间 </summary>
        public DateTime create_time { get; set; }
    }
}
