using NTKServer.Internal;
using X.PagedList;

namespace NTKServer.ViewModels.TradeAccount
{
    public class TradeStopSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public TradeStopSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<TradeStopSearchList> list { get; set; }

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }

        /// <summary> 正向排序還是逆向 </summary>
        public SortEnum dir { get; set; }
        
    }

    public class TradeStopSearchFilter
    {
        [Where("like", "sub_account")]
        public string? sub_account { get; set; }

        [Where("=", "status")]
        public sbyte status { get; set; } = 0;
    }

    public class TradeStopSearchList
    {
        public string? sn { get; set; }
        public byte dir { get; set; }
        public string? stock_code { get; set; }
        public string? stock_name { get; set; }
        public decimal? price { get; set; }
        public DateTime? cancel_datetime { get; set; }
        public long? cancel_datetime_time_zone { get; set; }
        public string? cancel_by { get; set; }
        public int volume { get; set; }
        public int? succeed_volume { get; set; }
        public int? free_volume { get; set; }
        public int? cancel_volume { get; set; }
    }
}
