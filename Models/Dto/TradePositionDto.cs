using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class TradePositionDto
    {
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 股票代码 </summary>
        public string stock_code { get; set; }
        /// <summary> 股票名称 </summary>
        public string stock_name { get; set; }
        /// <summary> 持單類型 1.多單 -1 空單 </summary>
        public int stock_type { get; set; }
        /// <summary> 市场别 </summary>
        public string market { get; set; }
        /// <summary> 持有数量 </summary>
        public int holding_volume { get; set; }
        /// <summary> 止損掛單數量 </summary>
        public int stop_lose_pos { get; set; }
        /// <summary> 本日新倉部位 </summary>
        public int new_pos { get; set; }
        /// <summary> 平倉未成交冻结部份 </summary>
        public int close_pos { get; set; }
        /// <summary> 市价 </summary>
        public decimal lastprice { get; set; }
        /// <summary> 市值 </summary>
        public decimal total { get; set; }
        /// <summary> 成本总值(累加) </summary>
        public decimal cost_purchase { get; set; }
        /// <summary> 累加持有量(不扣除) </summary>
        public int cost_volume { get; set; }
        /// <summary> 成本价 </summary>
        public decimal cost_price { get; set; }
        /// <summary> 实盘数量 </summary>
        public int live_volume { get; set; }
        /// <summary> 实盘成本价 </summary>
        public decimal live_cost { get; set; }
    }
}
