using NTKServer.Internal;

namespace NTKServer.ViewModels.ReviewBorrow
{
    public class ReviewBorrowList
    {
		/// <summary> account: 会员帐号 </summary>
		public string account { get; set; }

		/// <summary> nickname: 会员姓名 </summary>
		public string nickname { get; set; }

		/// <summary> pk: # </summary>
		public int pk { get; set; }

        /// <summary> order_id: 订单号码 </summary>
        public string order_id { get; set; }

        /// <summary> market: 交易市场别 </summary>
        public string market { get; set; }

        /// <summary> borrow_type: 交易帐号类型 </summary>
        public string borrow_type { get; set; }

        /// <summary> deposit_money: 保证金 </summary>
        public decimal deposit_money { get; set; }

        /// <summary> borrow_money: 融资金额 </summary>
        public decimal borrow_money { get; set; }

        /// <summary> borrow_interest: 融资管理费 </summary>
        public decimal borrow_interest { get; set; }

        /// <summary> create_time: 申请时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> begin_time: 操盘起始时间 </summary>
        public DateTime begin_time { get; set; }

        /// <summary> end_time: 操盘结束时间 </summary>
        public DateTime end_time { get; set; }

    }
}
