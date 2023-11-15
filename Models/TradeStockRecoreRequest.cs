using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace Models.Dto
{
    public class TradeStockRecoreRequest
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
        /// <summary> 操作人员 </summary>
        public string reviewer { get; set; }
        /// <summary> 市場 </summary>
        public string market { get; set; }
        /// <summary> 股票代码 </summary>
        public string stock_code { get; set; }
        /// <summary> 股票名称 </summary>
        public string stock_name { get; set; }
        /// <summary> 交割单号 </summary>
        public int? trade_deal_fk { get; set; }
        /// <summary> 交易種類 </summary>
        public int order_type { get; set; }
        public object[]? list { get; set; }

        /// <summary> 交易时间 </summary>
        public DateTime create_datetime { get; set; }
    }
}
