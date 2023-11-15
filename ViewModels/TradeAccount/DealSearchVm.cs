using NTKServer.Internal;
using X.PagedList;

namespace NTKServer.ViewModels.TradeAccount
{
    public class DealSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public DealSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<DealSearchList> list { get; set; }

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }

        /// <summary> 正向排序還是逆向 </summary>
        public SortEnum dir { get; set; }
    }

    public class DealSearchFilter
    {
        [Where("like", "sub_account")]
        public string? sub_account { get; set; }

    }

    public class DealSearchList
    {
        public uint pk { get; set; }
        public string? trade_order_sn { get; set; }
        public byte dir { get; set; }
        public string? stock_code { get; set; }
        public string? stock_name { get; set; }
        public decimal? final_price { get; set; }
        public int? final_volume { get; set; }
        public decimal? total_amount { get; set; }
        public DateTime? create_datetime { get; set; }
        public DateTime? create_datetime_time_zone { get; set; }
        public decimal? total_pay_system { get; set; }
        public decimal? total_pay_user { get; set; }
        public decimal? handling_fee { get; set; }
        public decimal? transfer_fee { get; set; }
        public decimal? other_fee { get; set; }
    }
}
