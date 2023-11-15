using NTKServer.Internal;
using NTKServer.ViewModels.BorrowRequest;
using X.PagedList;

namespace NTKServer.ViewModels.BorrowAddfinancing
{
    public class BorrowAddfinancingSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public BorrowAddfinancingSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<BorrowAddfinancingSearchList> list { get; set; }

        /// <summary> 分页 </summary>
        public int page { get; set; } = 1;

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }
    }

    public class BorrowAddfinancingSearchFilter
    {
        [Where("=", "member_fk")]
        public int? member_fk { get; set; }

        [Where("like", "member_real_name")]
        public string? member_real_name { get; set; }

        [Where("like", "sub_account")]
        public string? sub_account { get; set; }

    }

    public class BorrowAddfinancingSearchList
    {
        public uint pk { get; set; }
        public uint member_fk { get; set; }
        public string? member_real_name { get; set; }
        public string? sub_account { get; set; }
        public string borrow_plan { get; set; }
        public decimal? multiple { get; set; }
        public DateTime? end_time { get; set; }
        public decimal last_deposit_money { get; set; }
        public decimal last_borrow_money { get; set; }
        public decimal money { get; set; }
        public string? currency { get; set; }
        public decimal? exchange { get; set; }
        public decimal? freeze { get; set; }
        public DateTime? add_time { get; set; }
        public decimal borrow_interest { get; set; }
        public DateTime? verify_time { get; set; }
        public string? target_name { get; set; }
        public sbyte status { get; set; }
    }
}
