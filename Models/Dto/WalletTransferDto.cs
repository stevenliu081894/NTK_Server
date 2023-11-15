using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletTransferDto
    {
        /// <summary> 管理员id </summary>
        public int admin_user_fk { get; set; }
        /// <summary> 会员FK </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 订单号 </summary>
        public string order_no { get; set; }
        /// <summary> 转账金额 </summary>
        public decimal money { get; set; }
        /// <summary> 添加时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 添加ip </summary>
        public string create_ip { get; set; }
        /// <summary> 信息说明 </summary>
        public string info { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
    }
}
