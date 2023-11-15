using NTKServer.Internal;
using NTKServer.ViewModels.AdminLogin;
using X.PagedList;

namespace NTKServer.ViewModels.TradeAccount
{
    public class TradeAccountSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public TradeAccountSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<TradeAccountSearchList> list { get; set; }

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }

        /// <summary> 正向排序還是逆向 </summary>
        public SortEnum dir { get; set; }
    }

    public class TradeAccountSearchFilter
    {
        [Where("like", "ip")]
        public string? ip { get; set; }

        [Where("like", "login_account")]
        public string? login_account { get; set; }
    }

    public class TradeAccountSearchList
    {
        public string? sub_account { get; set; }
        public byte status { get; set; }
        public byte? type { get; set; }
        public string? member_name { get; set; }
        public string? member_username { get; set; }
        public string? member_name_username
        {
            get
            {
                return $"{member_name}<br>{member_username}";
            }
        }
        public string? member_mobile { get; set; }
        public string? loan_type { get; set; }
        public string? market { get; set; }
        public string? currency { get; set; }
        public string? market_currency
        {
            get
            {
                return $"{market}<br>{currency}";
            }
        }
        public DateTime? begin_time { get; set; }
        public DateTime? end_time { get; set; }
        public string? begin_end_time
        {
            get
            {

                return (end_time.HasValue && end_time.Value.AddDays(1) > DateTime.Now) ?
                    $"{begin_time}<br><font color='red'>{end_time}</font>" : $"{begin_time}<br>{end_time}";
            }
        }
        public decimal loan_money { get; set; }
        public decimal margin { get; set; }
        public string? loan_money_margin
        {
            get
            {
                return $"{loan_money}<br>{margin}";
            }
        }
        public decimal? total { get; set; }
        public decimal mem_money { get; set; }
        public decimal frozen_money { get; set; }
        public string? mem_money_frozen_money
        {
            get
            {
                return $"{mem_money}<br>{frozen_money}";
            }
        }
        public decimal? assets
        {
            get
            {
                return total + mem_money;
            }
        }
        /// <summary>
        /// 預警線
        /// </summary>
        public decimal? warningline { get; set; }
        public decimal? warningline_distance
        {
            get
            {
                return assets - warningline;
            }
        }
        public string? warningline_warningline_distance
        {
            get
            {
                return $"{warningline}<br>{warningline_distance}";
            }
        }
        /// <summary>
        /// 平倉線
        /// </summary>
        public decimal? breakline { get; set; }
        public decimal? breakline_distance
        {
            get
            {
                return assets - breakline;
            }
        }
        public string? breakline_breakline_distance
        {
            get
            {
                return $"{breakline}<br>{breakline_distance}";
            }
        }
    }
}
