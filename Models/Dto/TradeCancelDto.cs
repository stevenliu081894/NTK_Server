using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class TradeCancelDto
    {
        /// <summary> 委托单号 </summary>
        public string trade_order_sn { get; set; }
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 撤单单号 </summary>
        public string sn { get; set; }
        /// <summary> 交易市场别(US,HK,CN,TW) </summary>
        public string market { get; set; }
        /// <summary> 股票代码 </summary>
        public string stock_code { get; set; }
        /// <summary> 股票名称 </summary>
        public string stock_name { get; set; }
        /// <summary> 请求撤销数量 </summary>
        public int request_volume { get; set; }
        /// <summary> 已取消数量 </summary>
        public int cancel_volume { get; set; }
        /// <summary> 委托 IP </summary>
        public string order_ip { get; set; }
        /// <summary> 操作端版本 </summary>
        public string order_client { get; set; }
        /// <summary> 1 用户自己 2.系统 3.后台 </summary>
        public int cancel_type { get; set; }
        /// <summary> 取消时间 </summary>
        public DateTime cancel_datetime { get; set; }
        /// <summary> 取消者 </summary>
        public string cancel_by { get; set; }
    }
}
