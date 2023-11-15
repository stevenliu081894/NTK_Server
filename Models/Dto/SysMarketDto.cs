using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class SysMarketDto
    {
        /// <summary> 市场代码 </summary>
        public string code { get; set; }
        /// <summary> 交易币种 </summary>
        public string currency { get; set; }
        /// <summary> 是否启用 </summary>
        public bool enable { get; set; }
        /// <summary> 市场名称 </summary>
        public string name { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 买入手续费 </summary>
        public decimal buy_fee { get; set; }
        /// <summary> 卖出手续费 </summary>
        public decimal sell_fee { get; set; }
        /// <summary> 买入最低收续费 </summary>
        public decimal min_buy_fee { get; set; }
        /// <summary> 卖出最低手续费 </summary>
        public decimal min_sell_fee { get; set; }
		/// <summary> 新用户预设交易股票 </summary>
		public string default_stock_code { get; set; }
	}
}
