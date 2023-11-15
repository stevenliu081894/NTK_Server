using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletCouponRecordDto
    {
        /// <summary> 交易用户 </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 幣別 </summary>
        public string currency { get; set; }
        /// <summary> 影响金额 </summary>
        public decimal affect { get; set; }
        /// <summary>  </summary>
        public decimal coupon_before { get; set; }
        /// <summary> 红包账户余额 </summary>
        public decimal coupon_after { get; set; }
        /// <summary> 消费红包资金类型 </summary>
        public int type { get; set; }
        /// <summary>  </summary>
        public int sub_type { get; set; }
        /// <summary> 详情 </summary>
        public string info { get; set; }
        /// <summary> 交易时间 </summary>
        public DateTime create_time { get; set; }
    }
}
