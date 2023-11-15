namespace NTKServer.ViewModels.HisAddfinancing
{
    public class HisAddfinancingEditVm
    {
        /// <summary> borrow_addfinancing: 审核结果 </summary>
        public int status { get; set; }

        /// <summary> vw_trade_account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> vw_trade_account: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> borrow_addfinancing: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> vw_trade_account: 市场 </summary>
        public string market { get; set; }

        /// <summary> vw_trade_account: 帐号类型 </summary>
        public string loan_type { get; set; }

        /// <summary> borrow_addfinancing: 编号 </summary>
        public int pk { get; set; }

        /// <summary> borrow_addfinancing: 扩大金额 </summary>
        public decimal money { get; set; }

        /// <summary> borrow_addfinancing: 管理费 </summary>
        public decimal borrow_interest { get; set; }

        /// <summary> borrow_addfinancing: 匯率 </summary>
        public decimal exchange { get; set; }

        /// <summary> borrow_addfinancing: 主钱包异动 </summary>
        public decimal freeze { get; set; }

        /// <summary> borrow_addfinancing: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> borrow_addfinancing: 审核时间 </summary>
        public DateTime verify_time { get; set; }

    }
}
