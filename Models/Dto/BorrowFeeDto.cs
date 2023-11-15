using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class BorrowFeeDto
    {
        /// <summary> 會員pk </summary>
        public int member_fk { get; set; }
        /// <summary> 交易子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 合約書FK </summary>
        public int borrow_fk { get; set; }
        /// <summary> 主键PK </summary>
        public int pk { get; set; }
        /// <summary> 业务类型 1.新合约 2.续期 </summary>
        public int type { get; set; }
        /// <summary> 管理费 </summary>
        public decimal borrow_fee { get; set; }
        /// <summary> 使用折扣券 </summary>
        public decimal use_coupon { get; set; }
        /// <summary> 实收金额 fee-coupon </summary>
        public decimal fee_received { get; set; }
        /// <summary> 合约时长 </summary>
        public int borrow_duration { get; set; }
        /// <summary> 发生时间 </summary>
        public DateTime create_time { get; set; }
    }
}
