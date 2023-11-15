using NTKServer.Internal;

namespace NTKServer.ViewModels.HisWalletRecharge
{
    public class HisWalletRechargeList
    {
        /// <summary> status: 审核结果 </summary>
        public int status { get; set; }

		/// <summary> status: 审核结果 </summary>
		public string statusTxt
		{
			get
			{
				return WalletRechargeConvertEnum.ConvertVerifyStatus(status);
			}
		}

		/// <summary> verify_admin_pk: 審核人員 </summary>
		public int verify_admin_pk { get; set; }

		/// <summary> admin_name: 審核人員名稱 </summary>
		public string admin_name { get; set; }

        /// <summary> verify_time: 審核時間 </summary>
        public DateTime verify_time { get; set; }

        /// <summary> reject_result: 審核失敗原因 </summary>
        public string reject_result { get; set; }

        /// <summary> order_no: 充值單號 </summary>
        public string order_no { get; set; }

        /// <summary> create_time: 充值申请时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> card: 收款卡号 </summary>
        public string card { get; set; }

        /// <summary> bank_name: 收款银行 </summary>
        public string bank_name { get; set; }

        /// <summary> payee: 收款人 </summary>
        public string payee { get; set; }

        /// <summary> line_bank: 充值信息 </summary>
        public string line_bank { get; set; }

        /// <summary> pk: 編號 </summary>
        public int pk { get; set; }

        /// <summary> type: 充值方式 </summary>
        public string type { get; set; }

        /// <summary> currency: 币别 </summary>
        public string currency { get; set; }

        /// <summary> money: 充值金额 </summary>
        public decimal money { get; set; }

        /// <summary> exchange: 转换汇率 </summary>
        public decimal exchange { get; set; }

        /// <summary> wallet_amount: 主钱包金额 </summary>
        public decimal wallet_amount { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

    }
}
