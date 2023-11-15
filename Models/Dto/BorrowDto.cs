using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class BorrowDto
    {
        /// <summary> 交易子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 融资类型:参照borrow_plan </summary>
        public int borrow_plan_fk { get; set; }
        /// <summary> 外键 会员FK </summary>
        public int member_fk { get; set; }
        /// <summary> 代理编号 </summary>
        public int agent_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 配资单号 </summary>
        public string order_id { get; set; }
        /// <summary> 配资状态 -1：待审核  0：未通过 1：使用中  2：已结束 3：已逾期  </summary>
        public int status { get; set; }
        /// <summary> 交易市场别 </summary>
        public string market { get; set; }
        /// <summary> 融资类型 </summary>
        public string borrow_type { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
        /// <summary> 保证金 </summary>
        public decimal deposit_money { get; set; }
        /// <summary> 股票初始资金 </summary>
        public decimal init_money { get; set; }
        /// <summary> 配资杠杆倍数 </summary>
        public int multiple { get; set; }
        /// <summary> 自动续期 </summary>
        public bool auto_renewal { get; set; }
        /// <summary> 融资金额 </summary>
        public decimal borrow_money { get; set; }
        /// <summary> 融资管理费 </summary>
        public decimal borrow_interest { get; set; }
        /// <summary> 还款类型(保留没作用) </summary>
        public int repayment_type { get; set; }
        /// <summary> 融资时长 </summary>
        public int borrow_duration { get; set; }
        /// <summary> 仓位限制(别的机制处理) </summary>
        public int position { get; set; }
        /// <summary> 融资费率 </summary>
        public decimal rate { get; set; }
        /// <summary> 融资收费总批次 </summary>
        public int total { get; set; }
        /// <summary> 交易时间 0 今日 1 下个交易日 </summary>
        public bool trading_time { get; set; }
        /// <summary> 简讯通知 </summary>
        public int loss_warn_sms_send { get; set; }
        /// <summary> 股票卖出总金额 </summary>
        public decimal stock_money { get; set; }
        /// <summary> 使用优惠券总额 </summary>
        public decimal total_coupon { get; set; }
        /// <summary> 交易费实收总金额 </summary>
        public decimal total_fee { get; set; }
        /// <summary> 总管理费 </summary>
        public decimal total_interest { get; set; }
        /// <summary> 配资申请时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 操盘起始时间 </summary>
        public DateTime begin_time { get; set; }
        /// <summary> 操盘结束时间 </summary>
        public DateTime end_time { get; set; }
        /// <summary> 审核时间 </summary>
        public DateTime verify_time { get; set; }
    }
}
