using NTKServer.Internal;

namespace NTKServer.ViewModels.UserTradePosition
{
    public class UserTradePositionList
    {
        /// <summary> sub_account: 子帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> stock_code: 股票代码 </summary>
        public string stock_code { get; set; }

        /// <summary> stock_name: 股票名称 </summary>
        public string stock_name { get; set; }

        /// <summary> stock_type: 持仓類型 </summary>
        public int stock_type { get; set; }

        /// <summary> profit: 盈亏 </summary>
        public decimal profit { get; set; }

        /// <summary> holding_volume: 持有数量 </summary>
        public int holding_volume { get; set; }

        /// <summary> lastprice: 现价 </summary>
        public decimal lastprice { get; set; }

        /// <summary> cost_price: 成本价 </summary>
        public decimal cost_price { get; set; }

        /// <summary> new_pos: 新倉凍結 </summary>
        public int new_pos { get; set; }

        /// <summary> close_pos: 掛單冻结 </summary>
        public int close_pos { get; set; }

        /// <summary> stop_lose_pos: 止損掛單 </summary>
        public int stop_lose_pos { get; set; }

        /// <summary> total: 总市值 </summary>
        public decimal total { get; set; }

    }
}
