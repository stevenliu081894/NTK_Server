using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class TradeMoneyCheckDto
    {
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 流水号 </summary>
        public string sn { get; set; }
        /// <summary> 帐务类型 0.提取 1.余额转出 </summary>
        public int type { get; set; }
        /// <summary> 状态 0.审核中 1.成功，2.失败 </summary>
        public int state { get; set; }
        /// <summary> 冻结金额(交易錢包) </summary>
        public decimal frozen { get; set; }
        /// <summary> 汇率 </summary>
        public decimal exchange { get; set; }
        /// <summary> 收款币种(交易錢包) </summary>
        public string currency { get; set; }
        /// <summary> 收款币别金额(主錢包) </summary>
        public decimal amount { get; set; }
        /// <summary> 请求时间 </summary>
        public DateTime request_time { get; set; }
        /// <summary> 同意人员 </summary>
        public string acccept_by { get; set; }
        /// <summary> 同意时间 </summary>
        public DateTime accept_time { get; set; }
    }
}
