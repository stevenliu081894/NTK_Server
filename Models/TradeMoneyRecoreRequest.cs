using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace Models.Dto
{
    public class TradeMoneyRecoreRequest
    {
        /// <summary> 会员pk </summary>
        public int member_fk { get; set; }
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 帐务类型 </summary>
        public int temp_id { get; set; }
        /// <summary> 流水号 </summary>
        public string sn { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
        /// <summary> 影响金额 </summary>
        public decimal affect { get; set; }
        /// <summary> 转换汇率 </summary>
        public decimal exchange { get; set; }
        /// <summary> 影响钱包金额 </summary>
        public decimal wallet_amount { get; set; }
        /// <summary> 操作人员 </summary>
        public string reviewer { get; set; }
        public object[]? list { get; set; }
        /// <summary> 交易时间 </summary>
        public DateTime create_datetime { get; set; }
    }
}
