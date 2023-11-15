using NTKServer.Internal;
using X.PagedList;

namespace NTKServer.ViewModels.TradeAccount
{
    public class TradePositionSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public TradePositionSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<TradePositionSearchList> list { get; set; }

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }

        /// <summary> 正向排序還是逆向 </summary>
        public SortEnum dir { get; set; }
    }

    public class TradePositionSearchFilter
    {
        [Where("like", "sub_account")]
        public string? sub_account { get; set; }

        [Where("=", "status")]
        public sbyte status { get; set; } = 0;
    }

    public class TradePositionSearchList
    {
        public string stock_code { get; set; }
        public string? stock_name { get; set; }
        public int? holding_volume { get; set; }
        public decimal? lastprice { get; set; }
        public decimal? cost_price_avg { get; set; }
        public decimal total { get; set; }
        public decimal? cost_price { get; set; }
        public decimal? profit { get; set; }
        public int? frozen_volume { get; set; }
        public int? sell_volume { get; set; }
    }
}
