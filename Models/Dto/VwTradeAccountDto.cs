using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class VwTradeAccountDto
    {
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary>  </summary>
        public decimal balance { get; set; }
        /// <summary>  </summary>
        public decimal warning_value { get; set; }
        /// <summary>  </summary>
        public decimal break_value { get; set; }
        /// <summary> 客户 PK </summary>
        public int member_fk { get; set; }
        /// <summary> 登入帐号 </summary>
        public string account { get; set; }
        /// <summary> 真实姓名 </summary>
        public string member_name { get; set; }
        /// <summary> 0--试玩 1--正式 2--实盘 </summary>
        public int type { get; set; }
        /// <summary> 交易市场别(US,HK,CN,TW) </summary>
        public string market { get; set; }
        /// <summary> 交易币种 </summary>
        public string currency { get; set; }
        /// <summary> 融资名称 </summary>
        public string loan_type { get; set; }
        /// <summary> 帐户余额 </summary>
        public decimal mem_money { get; set; }
        /// <summary> 冻结金额 </summary>
        public decimal frozen_money { get; set; }
        /// <summary> 初始保证金 </summary>
        public decimal margin { get; set; }
        /// <summary> 保证金剩余额 </summary>
        public decimal margin_float { get; set; }
        /// <summary> 配资金额 </summary>
        public decimal loan_money { get; set; }
        /// <summary> 客户时区 </summary>
        public string time_zone { get; set; }
        /// <summary> 起始时间 </summary>
        public DateTime begin_time { get; set; }
        /// <summary> 应结束时间 </summary>
        public DateTime end_time { get; set; }
        /// <summary> 关闭帐号时间 </summary>
        public DateTime close_time { get; set; }
        /// <summary> 状态 0.可交易 1.冻结 2.清算中 3.已结束 </summary>
        public int status { get; set; }
        /// <summary> 预警线 </summary>
        public decimal warningline { get; set; }
        /// <summary> 平仓线 </summary>
        public decimal breakline { get; set; }
        /// <summary> 预警短信通知时间 </summary>
        public DateTime notice_warning { get; set; }
        /// <summary> 结束短信通知时间 </summary>
        public DateTime notice_close { get; set; }
        /// <summary> 持仓总金额 </summary>
        public decimal position_value { get; set; }
    }
}
