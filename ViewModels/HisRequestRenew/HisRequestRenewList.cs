namespace NTKServer.ViewModels.HisRequestRenew
{
    public class HisRequestRenewList
    {
        /// <summary> verify_time: 审核时间 </summary>
        public DateTime verify_time { get; set; }

        /// <summary> status: 审核状态 </summary>
        public int status { get; set; }
		/// <summary> status: 审核结果 </summary>
		public string statusTxt
		{
			get
			{
				return BorrowRequestConvertEnum.ConvertVerifyStatus(status);
			}
		}

		/// <summary> borrow_fee: 管理费 </summary>
		public decimal borrow_fee { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> add_time: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> loan_type: 帐号类型 </summary>
        public string loan_type { get; set; }

        /// <summary> pk: 主键PK </summary>
        public int pk { get; set; }

        /// <summary> borrow_duration: 续期时长 </summary>
        public int borrow_duration { get; set; }

        /// <summary> trade_end_time: 原结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> new_end_time: 新到期日 </summary>
        public DateTime new_end_time { get; set; }

    }
}
