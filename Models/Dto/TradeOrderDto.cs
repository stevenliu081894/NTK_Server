using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class TradeOrderDto
    {
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 委托单号(B/S + YYMMDD + 6碼流水編) </summary>
        public string sn { get; set; }
        /// <summary> 股票代码 </summary>
        public string stock_code { get; set; }
        /// <summary> 股票名称 </summary>
        public string stock_name { get; set; }
        /// <summary> 交易市场别(US,HK,CN,TW) </summary>
        public string market { get; set; }
        /// <summary> 1--买 2--卖 </summary>
        public int dir { get; set; }
        /// <summary> 1.新倉 2.平倉 </summary>
        public int order_type { get; set; }
        /// <summary> 交易類型 1.限价單 2.市价單 3.止損單 </summary>
        public int price_type { get; set; }
        /// <summary> 委托价格 </summary>
        public decimal price { get; set; }
        /// <summary> 状态 0.委托，1.全部成交，2.已撤单 </summary>
        public int status { get; set; }
        /// <summary> 委托数量 </summary>
        public int volume { get; set; }
        /// <summary> 未成交数量 </summary>
        public int free_volume { get; set; }
        /// <summary> 已成交数量 </summary>
        public int succeed_volume { get; set; }
        /// <summary> 已取消数量 </summary>
        public int cancel_volume { get; set; }
        /// <summary> 委托时间 </summary>
        public DateTime order_time { get; set; }
        /// <summary> 委托 IP </summary>
        public string order_ip { get; set; }
        /// <summary> 操作端版本 </summary>
        public string order_client { get; set; }
        /// <summary> 委推来源 1.用户 2. 系统 </summary>
        public int order_source { get; set; }
        /// <summary> 取消时间 </summary>
        public DateTime cancel_datetime { get; set; }
        /// <summary> 1 用户自己 2.系统 3.后台 </summary>
        public int cancel_type { get; set; }
        /// <summary> 取消者 </summary>
        public string cancel_by { get; set; }
        /// <summary> 0.无实盘 1.实盘 </summary>
        public int live_mode { get; set; }
        /// <summary> 实盘交易单号 </summary>
        public string live_ordersn { get; set; }
        /// <summary> 实盘委托数量 </summary>
        public int live_request { get; set; }
        /// <summary> 实盘成交数量 </summary>
        public int live_succeed { get; set; }
        /// <summary> 实盘平均成交价 </summary>
        public decimal live_price { get; set; }
    }
}
