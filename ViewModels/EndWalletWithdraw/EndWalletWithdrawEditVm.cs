
namespace NTKServer.ViewModels.EndWalletWithdraw
{
    public class EndWalletWithdrawEditVm
    {
        /// <summary> wallet_withdraw: 審核時間 </summary>
        public DateTime verify_time { get; set; }

        /// <summary> wallet_withdraw: 審核人員 </summary>
        public int verify_admin_pk { get; set; }

        /// <summary> wallet_withdraw: 状态 </summary>
        public int status { get; set; }

        /// <summary> wallet_withdraw: 審核備註 </summary>
        public string reject_result { get; set; }

        /// <summary> wallet_withdraw: 申请时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> wallet_withdraw: 申请编号 </summary>
        public string order_no { get; set; }

        /// <summary> wallet_withdraw: 金额 </summary>
        public decimal wallet_amount { get; set; }

        /// <summary> wallet_withdraw: 转换汇率 </summary>
        public decimal exchange { get; set; }

        /// <summary> wallet_withdraw: 提现币别 </summary>
        public string currency { get; set; }

        /// <summary> wallet_withdraw: 提现金额 </summary>
        public decimal money { get; set; }

		/// <summary> bank: 收款银行 </summary>
		public string bank { get; set; }

		/// <summary> branch: 收款分行 </summary>
		public string branch { get; set; }

		/// <summary> member_bank: 收款卡號 </summary>
		public string card { get; set; }

        /// <summary> member_bank: 卡号持有人 </summary>
        public string bank_account { get; set; }

        /// <summary> member: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member: 会员名称 </summary>
        public string nickname { get; set; }

    }
}
