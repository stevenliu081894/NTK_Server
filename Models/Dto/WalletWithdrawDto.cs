using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletWithdrawDto
    {
        /// <summary> 用户 </summary>
        public int member_fk { get; set; }
        /// <summary> 收款卡号 </summary>
        public string member_bank_fk { get; set; }
        /// <summary> 编号 </summary>
        public int pk { get; set; }
        /// <summary> 申请编号 </summary>
        public string order_no { get; set; }
        /// <summary> 主钱包金额 </summary>
        public decimal wallet_amount { get; set; }
        /// <summary> 汇率 </summary>
        public decimal exchange { get; set; }
        /// <summary> 提现币别 </summary>
        public string currency { get; set; }
        /// <summary> 提现资金 </summary>
        public decimal money { get; set; }
        /// <summary> 手续费 </summary>
        public decimal fee { get; set; }
        /// <summary> 提现状态0处理中 1 提现成功 2 提现失败 3提现退回 </summary>
        public int status { get; set; }
        /// <summary> 备注 </summary>
        public string note { get; set; }
        /// <summary> 申请时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 申请IP </summary>
        public string create_ip { get; set; }
        /// <summary> 審核人員 </summary>
        public int verify_admin_pk { get; set; }
        /// <summary> 審核時間 </summary>
        public DateTime verify_time { get; set; }
        /// <summary> 審核失敗原因 </summary>
        public string reject_result { get; set; }
    }
}
