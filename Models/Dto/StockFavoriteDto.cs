using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class StockFavoriteDto
    {
        /// <summary> 会员FK </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 股票代码 </summary>
        public string stock_code { get; set; }
        /// <summary> 市场别 </summary>
        public string market { get; set; }
    }
}
