using NTKServer.Internal;

namespace NTKServer.ViewModels.HisBorrow
{
    public class HisBorrowList
    {
        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> sub_account: 交易子帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> status: 审核结果 </summary>
        public int status { get; set; }

		/// <summary> status: 审核结果 </summary>
		public string statusTxt
		{
			get
			{
                return BorrowConvertEnum.ConvertBorrowStatus(status);
			}
		}

		/// <summary> market: 交易市场别 </summary>
		public string market { get; set; }

        /// <summary> borrow_type: 帐号类型 </summary>
        public string borrow_type { get; set; }

        /// <summary> init_money: 初始资金 </summary>
        public decimal init_money { get; set; }

        /// <summary> balance: 帐户余额 </summary>
        public decimal balance { get; set; }

        /// <summary> deposit_money: 保证金 </summary>
        public decimal deposit_money { get; set; }

        /// <summary> multiple: 帐号杠杆倍数 </summary>
        public int multiple { get; set; }

        /// <summary> auto_renewal: 自动续期 </summary>
        public bool auto_renewal { get; set; }

        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> order_id: 订单号码 </summary>
        public string order_id { get; set; }

        /// <summary> borrow_interest: 管理费 </summary>
        public decimal borrow_interest { get; set; }

        /// <summary> begin_time: 操盘起始时间 </summary>
        public DateTime begin_time { get; set; }

        /// <summary> end_time: 操盘结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> create_time: 申请时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> verify_time: 审核时间 </summary>
        public DateTime verify_time { get; set; }

    }
}
