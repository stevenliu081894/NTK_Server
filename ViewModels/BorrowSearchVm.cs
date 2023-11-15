using NTKServer.Internal;
using X.PagedList;

namespace NTKServer.ViewModels
{
    public class BorrowSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public BorrowSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<BorrowSearchList> list { get; set; }

        /// <summary> 分页 </summary>
        public int page { get; set; } = 1;

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }
    }

    public class BorrowSearchFilter
    {
        [Where("=", "member_fk")]
        public int? member_fk { get; set; }

        [Where("=", "status")]
        public int? status { get; set; }

        [Where("like", "member_username")]
        public string? member_username { get; set; }
        
        [Where("like", "member_real_name")]
        public string? member_real_name { get; set; }

        [Where("like", "order_id")]
        public string? order_id { get; set; }

    }

    public class BorrowSearchList
    {
        public uint pk { get; set; }
        public int? borrow_plan_fk { get; set; }
        public int member_fk { get; set; }
        public string member_username { get; set; }
        public string? member_real_name { get; set; }
        public string order_id { get; set; }
        public sbyte status { get; set; }
        public string? borrow_type { get; set; }
        public string? market { get; set; }
        public int borrow_duration { get; set; }
        public sbyte? auto_renewal { get; set; }
        public DateTime? begin_time { get; set; }
        public DateTime? end_time { get; set; }
        public decimal deposit_money { get; set; }
        public decimal borrow_money { get; set; }
        public sbyte multiple { get; set; }
        public decimal rate { get; set; }
        public decimal borrow_interest { get; set; }
        public decimal init_money { get; set; }
        public DateTime create_time { get; set; }
        public DateTime? verify_time { get; set; }
    }

    public class BorrowApply
    {
        public int pk { get; set; }
        public int borrow_plan_fk { get; set; }
        public int member_fk { get; set; }
        public string member_username { get; set; }
        public string member_real_name { get; set; }
        public string order_id { get; set; }
        public sbyte status { get; set; }
        public string borrow_type { get; set; }
        public string currency { get; set; }
        public string market { get; set; }
        public int borrow_duration { get; set; }
        public sbyte? auto_renewal { get; set; }
        public DateTime begin_time { get; set; }
        public DateTime end_time { get; set; }
        public decimal deposit_money { get; set; }
        public decimal borrow_money { get; set; }
        public sbyte multiple { get; set; }
        public decimal rate { get; set; }
        public decimal borrow_interest { get; set; }
        public string time_zone { get; set; }
        public decimal? warning_line { get; set; }
        public decimal? break_line { get; set; }
        public decimal init_money { get; set; }
        public decimal total_coupon { get; set; }
        public decimal total_fee { get; set; }
        public DateTime create_time { get; set; }
        public DateTime? verify_time { get; set; }
    }
}
