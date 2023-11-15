namespace NTKServer.ViewModels.SysMarket
{
    public class SysMarketList
    {
        /// <summary> code: 市场代码 </summary>
        public string code { get; set; }

        /// <summary> currency: 交易币种 </summary>
        public string currency { get; set; }

        /// <summary> enable: 是否启用 </summary>
        public bool enable { get; set; }

        /// <summary> name: 市场名称 </summary>
        public string name { get; set; }

        /// <summary> sort: 排序 </summary>
        public int sort { get; set; }

        /// <summary> buy_fee: 买入手续费 </summary>
        public decimal buy_fee { get; set; }

        /// <summary> sell_fee: 卖出手续费 </summary>
        public decimal sell_fee { get; set; }

        /// <summary> min_buy_fee: 买入最低收续费 </summary>
        public decimal min_buy_fee { get; set; }

        /// <summary> min_sell_fee: 卖出最低手续费 </summary>
        public decimal min_sell_fee { get; set; }

    }
}
