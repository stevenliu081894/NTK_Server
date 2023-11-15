using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminBankDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 1_银行帐号 2_虚拟币地址 3_第三方支付 </summary>
        public int type { get; set; }
        /// <summary> 适用国家 </summary>
        public string country { get; set; }
        /// <summary> 收款币别 </summary>
        public string currency { get; set; }
        /// <summary> 银行卡号 </summary>
        public string card { get; set; }
        /// <summary> 银行国际代码 </summary>
        public string SWIFT { get; set; }
        /// <summary> 所属银行 </summary>
        public string bank_name { get; set; }
        /// <summary> 开户行 </summary>
        public string open_bank { get; set; }
        /// <summary> 收款人 </summary>
        public string payee { get; set; }
        /// <summary> 说明 </summary>
        public string notes { get; set; }
        /// <summary> 状态 默认1：启用 0：禁用 </summary>
        public int status { get; set; }
        /// <summary> 银行卡照片 url </summary>
        public string image { get; set; }
        /// <summary> 可见等级 </summary>
        public string viplists { get; set; }
        /// <summary>  </summary>
        public int bankimgid { get; set; }
    }
}
