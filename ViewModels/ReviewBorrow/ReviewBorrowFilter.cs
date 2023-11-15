using NTKServer.Internal;

namespace NTKServer.ViewModels.ReviewBorrow
{
    public class ReviewBorrowFilter
    {
		/// <summary> account: 会员帐号 </summary>
		[Where("=", "member.account")]
		public string? account { get; set; }

		/// <summary> nickname: 会员姓名 </summary>
		[Where("LIKE", "member.nickname")]
		public string? nickname { get; set; }

		/// <summary> pk: # </summary>
		[Where("=", "pk")]
        public int? pk { get; set; }

        /// <summary> order_id: 订单号码 </summary>
        [Where("=", "order_id")]
        public string? order_id { get; set; }

        /// <summary> borrow_type: 交易帐号类型 </summary>
        [Where("=", "borrow_type")]
        public string? borrow_type { get; set; }

    }
}
