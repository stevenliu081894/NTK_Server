using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletRechargeDto
    {
        /// <summary> 用户 </summary>
        public int member_fk { get; set; }
        /// <summary> 代理编号 </summary>
        public int agent_id { get; set; }
        /// <summary>  </summary>
        public int admin_bank_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 订单号 </summary>
        public string order_no { get; set; }
        /// <summary> 充值金额(单位 分) </summary>
        public decimal money { get; set; }
		/// <summary> 转换汇率 </summary>
		public decimal exchange { get; set; }
        /// <summary> 充值方式 </summary>
        public string type { get; set; }
		/// <summary> 主钱包金额 </summary>
		public decimal wallet_amount { get; set; }
        /// <summary> 充值手续费（预留 单位 分） </summary>
        public decimal fee { get; set; }
        /// <summary> 提交时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> ip </summary>
        public string create_ip { get; set; }
        /// <summary> 线下支付银行信息 </summary>
        public string line_bank { get; set; }
        /// <summary> 0待处理 1 成功 2 失败 </summary>
        public int status { get; set; }
		/// <summary> 後台審核人員 </summary>
		public int verify_admin_pk { get; set; }
		/// <summary> 審核時間 </summary>
		public DateTime? verify_time { get; set; }
		/// <summary> 充值凭证图片地址 </summary>
		public string receipt_img { get; set; }
        /// <summary> 平台对公账号id </summary>
        public int charge_type_id { get; set; }
        /// <summary> 转账人 </summary>
        public string form_name { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
		/// <summary> 審核失敗原因 </summary>
		public string reject_result { get; set; } = "";
	}
}
