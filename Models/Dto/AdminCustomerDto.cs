using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminCustomerDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 客户名称 </summary>
        public string customer_name { get; set; }
        /// <summary> 是否启用 </summary>
        public bool enable { get; set; }
        /// <summary> 商户号 </summary>
        public string appkey { get; set; }
        /// <summary> checksum专用key </summary>
        public string business_code { get; set; }
        /// <summary> 讯息语系 </summary>
        public string lang { get; set; }
        /// <summary> 会员交易系统入口 </summary>
        public string app_url { get; set; }
        /// <summary> 合约起始日 </summary>
        public DateTime contract_start_time { get; set; }
        /// <summary> 合约到期日 </summary>
        public DateTime contract_end_time { get; set; }
        /// <summary> 点数汇率 </summary>
        public decimal exange { get; set; }
    }
}
