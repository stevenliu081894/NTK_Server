using NTKServer.Internal;
using NTKServer.ViewModels.BorrowAddfinancing;
using X.PagedList;

namespace NTKServer.ViewModels.TradeMoney
{
    public class TradeMoneySearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public TradeMoneySearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<TradeMoneySearchList> list { get; set; }

        /// <summary> 分页 </summary>
        public int page { get; set; } = 1;

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }
    }

    public class TradeMoneySearchFilter
    {
        [Where("=", "member_fk")]
        public int? member_fk { get; set; }

        [Where("like", "member_real_name")]
        public string? member_real_name { get; set; }

        [Where("like", "sub_account")]
        public string? sub_account { get; set; }

    }

    public class TradeMoneySearchList
    {
        public uint pk { get; set; }
        public int member_fk { get; set; }
        public string? member_real_name { get; set; }
        public string sub_account { get; set; }
        public string? currency { get; set; }
        public string? acccept_by { get; set; }
        public decimal? frozen { get; set; }
        public decimal balance { get; set; }
        public decimal warningline { get; set; }
        public decimal breakline { get; set; }
        public decimal? exchange { get; set; }
        public decimal amount { get; set; }
        public DateTime request_time { get; set; }
        public DateTime? accept_time { get; set; }
        public string? accept_by { get; set; }
        public sbyte state { get; set; }
    }
}
