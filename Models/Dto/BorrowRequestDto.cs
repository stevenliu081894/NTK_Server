using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class BorrowRequestDto
    {
        /// <summary> 融资类型 </summary>
        public int borrow_plan_fk { get; set; }
        /// <summary> 交易子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 配资FK </summary>
        public int borrow_fk { get; set; }
        /// <summary> 会员FK </summary>
        public int member_fk { get; set; }
        /// <summary> 主键PK </summary>
        public int pk { get; set; }
        /// <summary> 业务类型 1：续期申请  2：终止配资 </summary>
        public int type { get; set; }
        /// <summary> 管理费 </summary>
        public decimal borrow_fee { get; set; }
        /// <summary> 使用折扣券 </summary>
        public decimal use_coupon { get; set; }
        /// <summary> 实收金额 fee-coupon </summary>
        public decimal fee_received { get; set; }
        /// <summary> 续期时长 </summary>
        public int borrow_duration { get; set; }
        /// <summary> 新到期日 </summary>
        public DateTime new_end_time { get; set; }
        /// <summary> 审核状态 0_审核中 1_审核通过  2_审核不通过 </summary>
        public int status { get; set; }
        /// <summary> 申请时间 </summary>
        public DateTime add_time { get; set; }
        /// <summary> 审核时间 </summary>
        public DateTime verify_time { get; set; }
    }
}
