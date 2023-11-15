using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletPaymentDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 支付名称 </summary>
        public string pay_name { get; set; }
        /// <summary> 支付标识 </summary>
        public string pay_code { get; set; }
        /// <summary> 支付类型 </summary>
        public string pay_type { get; set; }
        /// <summary> 支付接口url </summary>
        public string pay_url { get; set; }
        /// <summary> 支付商户id </summary>
        public string pay_account { get; set; }
        /// <summary> 支付商户密钥 </summary>
        public string pay_tokenkey { get; set; }
        /// <summary> 商户后台异步url </summary>
        public string pay_Notice_url { get; set; }
        /// <summary> 商户前台同步url </summary>
        public string pay_Return_url { get; set; }
        /// <summary> 商户前台同步url </summary>
        public int pay_sort { get; set; }
        /// <summary> 状态 </summary>
        public bool status { get; set; }
        /// <summary> 时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 说明 </summary>
        public string notes { get; set; }
        /// <summary> 可见等级 </summary>
        public string viplists { get; set; }
    }
}
