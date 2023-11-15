using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletRecordDto
    {
        /// <summary> 交易用户 </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 资金类型 </summary>
        public int type { get; set; }
        /// <summary> 子类别 </summary>
        public int subtype { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
        /// <summary> 影响金额 </summary>
        public decimal affect { get; set; }
        /// <summary> 冻结金额 </summary>
        public decimal freeze { get; set; }
        /// <summary> 账户可用余额 </summary>
        public decimal balance { get; set; }
        /// <summary> 折扣金余额 </summary>
        public decimal coupon { get; set; }
        /// <summary> 纪录值 </summary>
        public string param { get; set; }
        /// <summary> 模板编号 </summary>
        public int templat_id { get; set; }
        /// <summary> 详情 </summary>
        public string info { get; set; }
        /// <summary> 交易时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 交易IP </summary>
        public string create_ip { get; set; }
    }
}
