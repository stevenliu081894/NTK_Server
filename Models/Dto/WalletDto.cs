using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletDto
    {
        /// <summary> 用户 </summary>
        public int member_fk { get; set; }
        /// <summary> 幣別 </summary>
        public string currency { get; set; }
        /// <summary> 账户金额 </summary>
        public decimal balance { get; set; }
        /// <summary> 冻结金额 </summary>
        public decimal freeze { get; set; }
        /// <summary> 保证金总额 </summary>
        public decimal margin { get; set; }
        /// <summary> 操盘总总额(總配資) </summary>
        public decimal operate_balance { get; set; }
        /// <summary> 财富扑满 </summary>
        public decimal richbox_balance { get; set; }
        /// <summary> 是否冻结 1 正常 0冻结账户 </summary>
        public bool status { get; set; }
        /// <summary> 折抵券 </summary>
        public decimal coupon { get; set; }
        /// <summary> 累计充值 </summary>
        public decimal total_recharge { get; set; }
        /// <summary> 累计提现 </summary>
        public decimal total_withdraw { get; set; }
        /// <summary> 最后更动时间 </summary>
        public DateTime last_update_time { get; set; }
    }
}
