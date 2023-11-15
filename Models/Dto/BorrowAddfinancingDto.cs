using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class BorrowAddfinancingDto
    {
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 配资ID </summary>
        public int borrow_fk { get; set; }
        /// <summary> 会员 </summary>
        public int member_fk { get; set; }
        /// <summary> 主键ID </summary>
        public int pk { get; set; }
        /// <summary> 幣別 </summary>
        public string currency { get; set; }
        /// <summary> 扩大配资金额 </summary>
        public decimal money { get; set; }
        /// <summary> 轉換匯率 </summary>
        public decimal exchange { get; set; }
        /// <summary> 主錢包支付额 </summary>
        public decimal freeze { get; set; }
        /// <summary> 融资倍数 </summary>
        public decimal multiple { get; set; }
        /// <summary> 融资管理费 </summary>
        public decimal borrow_interest { get; set; }
        /// <summary> 原始保证金总额 </summary>
        public decimal last_deposit_money { get; set; }
        /// <summary> 原始融资总金额 </summary>
        public decimal last_borrow_money { get; set; }
        /// <summary> 状态 0：待审核 1：审核通过 2：审核未通过 </summary>
        public int status { get; set; }
        /// <summary> 申请时间 </summary>
        public DateTime add_time { get; set; }
        /// <summary> 审核时间 </summary>
        public DateTime verify_time { get; set; }
        /// <summary> 操作者ID </summary>
        public int target_uid { get; set; }
        /// <summary> 操作者姓名 </summary>
        public string target_name { get; set; }
        /// <summary> 使用折扣券(保留栏位) </summary>
        public decimal coupon { get; set; }
    }
}
