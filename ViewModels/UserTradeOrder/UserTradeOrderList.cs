using NTKServer.Internal;

namespace NTKServer.ViewModels.UserTradeOrder
{
    public class UserTradeOrderList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> sub_account: 子帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> order_time: 委托时间 </summary>
        public DateTime order_time { get; set; }

        /// <summary> sn: 委托单号 </summary>
        public string sn { get; set; }

        /// <summary> stock_code: 股票代码 </summary>
        public string stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        public string stock_name { get; set; }

        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> dir: 方向 </summary>
        public int dir { get; set; }

        /// <summary> order_type: 委托类别 </summary>
        public int order_type { get; set; }

        /// <summary> price_type: 订单類型 </summary>
        public int price_type { get; set; }

        /// <summary> price: 委托价格 </summary>
        public decimal price { get; set; }

        /// <summary> status: 状态 </summary>
        public int status { get; set; }

        /// <summary> volume: 委托数量 </summary>
        public int volume { get; set; }

        /// <summary> free_volume: 未成交数量 </summary>
        public int free_volume { get; set; }

        /// <summary> succeed_volume: 已成交数量 </summary>
        public int succeed_volume { get; set; }

        /// <summary> cancel_volume: 已取消数量 </summary>
        public int cancel_volume { get; set; }

    }
}
