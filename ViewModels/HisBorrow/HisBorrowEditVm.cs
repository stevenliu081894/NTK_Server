namespace NTKServer.ViewModels.HisBorrow
{
    public class HisBorrowEditVm
    {
        /// <summary> vw_trade_account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> vw_trade_account: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> borrow: 交易子帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> borrow: 审核结果 </summary>
        public int status { get; set; }

        /// <summary> borrow: 交易市场别 </summary>
        public string market { get; set; }

        /// <summary> borrow: 帐号类型 </summary>
        public string borrow_type { get; set; }

        /// <summary> borrow: 初始资金 </summary>
        public decimal init_money { get; set; }

        /// <summary> vw_trade_account: 帐户余额 </summary>
        public decimal balance { get; set; }

        /// <summary> borrow: 保证金 </summary>
        public decimal deposit_money { get; set; }

        /// <summary> borrow: 帐号杠杆倍数 </summary>
        public int multiple { get; set; }

        /// <summary> borrow: 自动续期 </summary>
        public bool auto_renewal { get; set; }

        /// <summary> borrow: # </summary>
        public int pk { get; set; }

        /// <summary> borrow: 订单号码 </summary>
        public string order_id { get; set; }

        /// <summary> borrow: 管理费 </summary>
        public decimal borrow_interest { get; set; }

        /// <summary> borrow: 操盘起始时间 </summary>
        public DateTime begin_time { get; set; }

        /// <summary> borrow: 操盘结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> borrow: 申请时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> borrow: 审核时间 </summary>
        public DateTime verify_time { get; set; }

    }
}
