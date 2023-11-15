using NTKServer.Internal;
using X.PagedList;

namespace NTKServer.ViewModels.BorrowRequest
{
    public class StockRenewalSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public StockRenewalSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<StockRenewalSearchList> list { get; set; }

        /// <summary> 分页 </summary>
        public int page { get; set; } = 1;

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }
    }

    public class StockRenewalSearchFilter
    {
        [Where("=", "member_fk")]
        public int? member_fk { get; set; }
        
        [Where("=", "status")]
        public int? status { get; set; }

        [Where("like", "member_real_name")]
        public string? member_real_name { get; set; }
        
        [Where("like", "sub_account")]
        public string? sub_account { get; set; }
      
    }

    public class StockRenewalSearchList
    {
        public uint pk { get; set; }
        public int member_fk { get; set; }
        public string? member_real_name { get; set; }
        public string? sub_account { get; set; }
        public byte status { get; set; }
        public DateTime? trade_account_end_time { get; set; }
        public DateTime? add_time { get; set; }
        public DateTime? verify_time { get; set; }
        public string? borrow_plane_name { get; set; }
        public decimal borrow_fee { get; set; }
        public decimal? use_coupon { get; set; }
    }
}
