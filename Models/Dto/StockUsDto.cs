using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class StockUsDto
    {
        /// <summary> 股票代码 </summary>
        public string stock_code { get; set; }
        /// <summary> 股票名称 </summary>
        public string stock_name { get; set; }
        /// <summary> 市场别 </summary>
        public string market { get; set; }
        /// <summary> 人工設定：是否可交易 </summary>
        public bool enable { get; set; }
        /// <summary> 程式判定：是否永久关闭 </summary>
        public bool disable_alwayse { get; set; }
        /// <summary> 程式判定：是否適合交易的股票 </summary>
        public bool program_enable { get; set; }
        /// <summary> 程式判定結果說明 </summary>
        public string program_msg { get; set; }
        /// <summary> 啟用總開關(enable、disable_alwayse、program_enable 三者綜合) </summary>
        public bool main_switch { get; set; }
        /// <summary> -1:新上市 0:未关闭 1:系统条件 2:后台设置 </summary>
        public int close_reason { get; set; }
        /// <summary> 开放交易日 </summary>
        public DateTime opentrade { get; set; }
        /// <summary> 最後更新时间 </summary>
        public DateTime update_datetime { get; set; }
        /// <summary> 昨日收盘价 </summary>
        public decimal yclose { get; set; }
        /// <summary> 涨停价 </summary>
        public decimal limitbuy { get; set; }
        /// <summary> 跌停价 </summary>
        public decimal limitsell { get; set; }
        /// <summary> 最後价格 </summary>
        public decimal final_price { get; set; }
        /// <summary> 成交量 </summary>
        public int volume { get; set; }
        /// <summary>  </summary>
        public string full_info { get; set; }
    }
}
