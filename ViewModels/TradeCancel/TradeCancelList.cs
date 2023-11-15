using NTKServer.Internal;

namespace NTKServer.ViewModels.TradeCancel
{
    public class TradeCancelList
    {
        /// <summary> cancel_datetime: 撤銷时间 </summary>
        public DateTime cancel_datetime { get; set; }

        /// <summary> sub_account: 子帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> sn: 撤单单号 </summary>
        public string sn { get; set; }

        /// <summary> trade_order_sn: 原始委托单 </summary>
        public string trade_order_sn { get; set; }

        /// <summary> stock_code: 股票代码 </summary>
        public string stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        public string stock_name { get; set; }

        /// <summary> cancel_volume: 撤銷数量 </summary>
        public int cancel_volume { get; set; }

        /// <summary> cancel_type: 1 用户自己 2.系统 3.后台 </summary>
        public int cancel_type { get; set; }

    }
}
