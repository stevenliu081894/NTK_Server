using NTKServer.Internal;

namespace NTKServer.ViewModels.BorrowAddmoney
{
    public class BorrowAddmoneyFilter
    {
        /// <summary> sub_account: 交易帐号 </summary>
        [Where("=", "borrow_addmoney.sub_account")]
        public string? sub_account { get; set; }

        /// <summary> market: 市场 </summary>
        [Where("=", "vw_trade_account.market")]
        public string? market { get; set; }

        /// <summary> member_fk: 会员编号 </summary>
        [Where("=", "borrow_addmoney.member_fk")]
        public int? member_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        [Where("=", "vw_trade_account.account")]
        public string? account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        [Where("LIKE", "vw_trade_account.member_name")]
        public string? member_name { get; set; }

    }
}
