namespace NTKServer.ViewModels.StockUs
{
    public class StockUsList
    {
        /// <summary> stock_code: 股票代码 </summary>
        public string stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        public string stock_name { get; set; }

        /// <summary> enable: 人工设置可交易 </summary>
        public bool enable { get; set; }

        /// <summary> disable_alwayse: 永久关闭 </summary>
        public bool disable_alwayse { get; set; }

        /// <summary> program_enable: 筛选限制交易 </summary>
        public bool program_enable { get; set; }

        /// <summary> main_switch: 可交易總開關 </summary>
        public bool main_switch { get; set; }

        /// <summary> program_msg: 关闭交易原因 </summary>
        public string program_msg { get; set; }

        /// <summary> opentrade: 开放交易日 </summary>
        public string opentrade { get; set; }

        /// <summary> update_datetime: 最後更新时间 </summary>
        public DateTime update_datetime { get; set; }

        /// <summary> yclose: 昨日收盘价 </summary>
        public decimal yclose { get; set; }

        /// <summary> final_price: 最後价格 </summary>
        public decimal final_price { get; set; }

        /// <summary> volume: 成交量 </summary>
        public int volume { get; set; }

        /// <summary> limitbuy: 涨停价 </summary>
        public decimal limitbuy { get; set; }

        /// <summary> limitsell: 跌停价 </summary>
        public decimal limitsell { get; set; }

    }
}
