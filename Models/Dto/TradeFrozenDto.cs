using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class TradeFrozenDto
    {
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 委托id </summary>
        public int trade_order_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 说明 </summary>
        public string info { get; set; }
        /// <summary> 0.委托买股 1.出金申请 </summary>
        public int type { get; set; }
        /// <summary> 买进股数 </summary>
        public int frozen_volume { get; set; }
        /// <summary> 冻结金额 </summary>
        public decimal frozen_money { get; set; }
        /// <summary> 冻结时间 </summary>
        public DateTime frozen_datetime { get; set; }
    }
}
