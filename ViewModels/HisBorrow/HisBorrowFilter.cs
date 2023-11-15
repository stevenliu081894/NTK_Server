using NTKServer.Internal;

namespace NTKServer.ViewModels.HisBorrow
{
    public class HisBorrowFilter
    {
        /// <summary> account: 会员帐号 </summary>
        [Where("=", "t.account")]
        public string? account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        [Where("LIKE", "t.member_name")]
        public string? member_name { get; set; }

        /// <summary> sub_account: 交易子帐号 </summary>
        [Where("=", "t.sub_account")]
        public string? sub_account { get; set; }

        /// <summary> borrow_type: 帐号类型 </summary>
        [Where("=", "t.borrow_type")]
        public string? borrow_type { get; set; }

        /// <summary> order_id: 订单号码 </summary>
        [Where("=", "t.order_id")]
        public string? order_id { get; set; }

    }
}
