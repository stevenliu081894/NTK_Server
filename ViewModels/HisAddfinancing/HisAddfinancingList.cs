namespace NTKServer.ViewModels.HisAddfinancing
{
    public class HisAddfinancingList
    {
        /// <summary> status: 审核结果 </summary>
        public int status { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> loan_type: 帐号类型 </summary>
        public string loan_type { get; set; }

        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> money: 扩大金额 </summary>
        public decimal money { get; set; }

        /// <summary> borrow_interest: 管理费 </summary>
        public decimal borrow_interest { get; set; }

        /// <summary> exchange: 匯率 </summary>
        public decimal exchange { get; set; }

        /// <summary> freeze: 主钱包异动 </summary>
        public decimal freeze { get; set; }

        /// <summary> add_time: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> verify_time: 审核时间 </summary>
        public DateTime verify_time { get; set; }

    }
}
