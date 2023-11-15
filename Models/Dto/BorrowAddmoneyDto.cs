using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class BorrowAddmoneyDto
    {
        /// <summary> 子帐号 </summary>
        public string sub_account { get; set; }
        /// <summary> 会员FK </summary>
        public int member_fk { get; set; }
        /// <summary> 主键PK </summary>
        public int pk { get; set; }
        /// <summary> 幣別 </summary>
        public string currency { get; set; }
        /// <summary> 轉換匯率 </summary>
        public decimal exchange { get; set; }
        /// <summary> 保证金(追加金额) </summary>
        public decimal money { get; set; }
        /// <summary> 主錢包凍結額 </summary>
        public decimal freeze { get; set; }
        /// <summary> 状态 0_待审核，1_审核通过，2_审核失败 </summary>
        public int status { get; set; }
        /// <summary> 添加时间 </summary>
        public DateTime add_time { get; set; }
        /// <summary> 审核时间 </summary>
        public DateTime verify_time { get; set; }
        /// <summary> 審核者ID </summary>
        public int target_uid { get; set; }
        /// <summary> 審核者姓名 </summary>
        public string target_name { get; set; }
    }
}
