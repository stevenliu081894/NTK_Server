using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class TradeDealDto
    {
        /// <summary> 成交PK </summary>
        public int pk { get; set; }
        /// <summary> 成交单号 [CODE: B/S/C (委买/委卖/取消)][YY][PK 8码,前面补0)] </summary>
        public string deal_id { get; set; }
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 委托单号 </summary>
        public string trade_order_sn { get; set; }
        /// <summary> 股票代码 </summary>
        public string stock_code { get; set; }
        /// <summary> 股票名称 </summary>
        public string stock_name { get; set; }
        /// <summary> 1. 新倉  2.平倉 </summary>
        public int order_type { get; set; }
        /// <summary> 市场别 </summary>
        public string market { get; set; }
        /// <summary> 1--买 2--卖 </summary>
        public int dir { get; set; }
        /// <summary> 成交价格 </summary>
        public decimal final_price { get; set; }
        /// <summary> 成交股数 </summary>
        public int final_volume { get; set; }
        /// <summary> 成交时间 </summary>
        public DateTime create_datetime { get; set; }
        /// <summary> 币种 </summary>
        public string currency { get; set; }
        /// <summary> 总支付(成交额+-手续费) </summary>
        public decimal total_pay { get; set; }
        /// <summary> 成交总金额 </summary>
        public decimal total_amount { get; set; }
        /// <summary> 总手续费 </summary>
        public decimal total_cost { get; set; }
        /// <summary> 手续费 </summary>
        public decimal handling_fee { get; set; }
        /// <summary> 过户费 </summary>
        public decimal transfer_fee { get; set; }
        /// <summary> 印花税 </summary>
        public decimal stamp_fee { get; set; }
        /// <summary> 其他费用 </summary>
        public decimal other_fee { get; set; }
    }
}
