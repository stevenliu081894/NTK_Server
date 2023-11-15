namespace NTKServer.ViewModels.HisAddmoney
{
    public class HisAddmoneyEditVm
    {
        /// <summary> vw_trade_account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> vw_trade_account: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> borrow_addmoney: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> vw_trade_account: 交易号类型 </summary>
        public string loan_type { get; set; }

        /// <summary> vw_trade_account: 市场 </summary>
        public string market { get; set; }

        /// <summary> borrow_addmoney: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> borrow_addmoney: 追加金额 </summary>
        public decimal money { get; set; }

        /// <summary> borrow_addmoney: 审核结果 </summary>
        public int status { get; set; }

        /// <summary> borrow_addmoney: 审核时间 </summary>
        public DateTime verify_time { get; set; }

        /// <summary> borrow_addmoney: 審核者姓名 </summary>
        public string target_name { get; set; }

    }
}
