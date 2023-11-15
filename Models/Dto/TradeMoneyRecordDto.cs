using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class TradeMoneyRecordDto
    {
        /// <summary> 会员pk </summary>
        public int member_fk { get; set; }
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 交割单号 </summary>
        public int? trade_deal_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 流水号 </summary>
        public string sn { get; set; }
        /// <summary> 帐务类型 </summary>
        public int temp_id { get; set; }
        /// <summary> 1.帐增加 -1.帐减少 </summary>
        public int op { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
        /// <summary> 钱包余额 </summary>
        public decimal balance { get; set; }
        /// <summary> 影响金额 </summary>
        public decimal affect { get; set; }
        /// <summary> 转换汇率 </summary>
        public decimal exchange { get; set; }
        /// <summary> 影响钱包金额 </summary>
        public decimal wallet_amount { get; set; }
        /// <summary> 说明 </summary>
        public string info { get; set; }
        /// <summary> 操作人员 </summary>
        public string reviewer { get; set; }
        /// <summary> 交易时间 </summary>
        public DateTime create_datetime { get; set; }
        /// <summary> 市场 </summary>
        public string market { get; set; }
        /// <summary> 1.新倉 2.平倉 </summary>
        public int? order_type { get; set; }
        /// <summary> 股票代码 </summary>
        public string? stock_code { get; set; }
        /// <summary> 股票名称 </summary>
        public string? stock_name { get; set; }
    }
}
