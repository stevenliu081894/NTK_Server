using NTKServer.Internal;
using X.PagedList;

namespace NTKServer.ViewModels.TradeAccount
{
    public class TradeEntrustedSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public TradeEntrustedSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<TradeEntrustedSearchList> list { get; set; }

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }

        /// <summary> 正向排序還是逆向 </summary>
        public SortEnum dir { get; set; }
    }

    public class TradeEntrustedSearchFilter
    {
        [Where("like", "sub_account")]
        public string? sub_account { get; set; }

        [Where("=", "status")]
        public sbyte status { get; set; } = 1;
    }

    public class TradeEntrustedSearchList
    {
        public string? sn { get; set; }
        public byte dir { get; set; }
        public byte? status { get; set; }
        public byte? order_source { get; set; }
        public string? stock_code { get; set; }
        public string? stock_name { get; set; }
        public decimal? price { get; set; }
        public int volume { get; set; }
        public int? free_volume { get; set; }
        public int succeed_volume { get; set; }
        public DateTime? order_time { get; set; }
        public DateTime? order_time_time_zone { get; set; }
        public string? order_ip { get; set; }
        public string? cancel_by { get; set; }
        public string? market { get; set; }
        public string? sub_account { get; set; }
    }
}
