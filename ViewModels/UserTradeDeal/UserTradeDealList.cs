using NTKServer.Internal;

namespace NTKServer.ViewModels.UserTradeDeal
{
    public class UserTradeDealList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> sub_account: 子帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> deal_id: 成交单号 </summary>
        public string deal_id { get; set; }

        /// <summary> trade_order_sn: 原委托单号 </summary>
        public string trade_order_sn { get; set; }

        /// <summary> stock_code: 股票代码 </summary>
        public string stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        public string stock_name { get; set; }

        /// <summary> order_type: 訂單類型 </summary>
        public int order_type { get; set; }

        /// <summary> dir: 方向 </summary>
        public int dir { get; set; }

        /// <summary> final_price: 成交价格 </summary>
        public decimal final_price { get; set; }

        /// <summary> final_volume: 成交股数 </summary>
        public int final_volume { get; set; }

        /// <summary> create_datetime: 成交时间 </summary>
        public DateTime create_datetime { get; set; }

        /// <summary> total_pay: 总支付 </summary>
        public decimal total_pay { get; set; }

        /// <summary> total_cost: 总手续费 </summary>
        public decimal total_cost { get; set; }

    }
}
