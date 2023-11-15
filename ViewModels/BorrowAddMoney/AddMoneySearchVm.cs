using NTKServer.Internal;
using NTKServer.ViewModels.BorrowAddfinancing;
using X.PagedList;

namespace NTKServer.ViewModels.BorrowAddMoney
{
    public class AddMoneySearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public AddMoneySearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<AddMoneySearchList> list { get; set; }

        /// <summary> 分页 </summary>
        public int page { get; set; } = 1;

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }
    }

    public class AddMoneySearchFilter
    {
        [Where("=", "member_fk")]
        public int? member_fk { get; set; }

        [Where("like", "member_real_name")]
        public string? member_real_name { get; set; }

        [Where("like", "sub_account")]
        public string? sub_account { get; set; }

        [Where("=", "status")]
        public int? status { get; set; }

    }

    public class AddMoneySearchList
    {
        public uint pk { get; set; }
        public int member_fk { get; set; }
        public string? member_real_name { get; set; }
        public string sub_account { get; set; }
        public sbyte status { get; set; }
        public string currency { get; set; }
        public decimal money { get; set; }
        public decimal balance { get; set; }
        public decimal warningline { get; set; }
        public decimal breakline { get; set; }
        public decimal? exchange { get; set; }
        public decimal freeze { get; set; }
        public DateTime add_time { get; set; }
        public DateTime? verify_time { get; set; }
        public string? target_name { get; set; }
    }
}
