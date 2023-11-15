namespace NTKServer.ViewModels.HisTradeMoneyCheck
{
    public class HisTradeMoneyCheckReview
    {
        /// <summary> request_time: 请求时间 </summary>
        public DateTime request_time { get; set; }

        /// <summary> accept_time: 审核时间 </summary>
        public DateTime accept_time { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> sn: 申请单号 </summary>
        public string sn { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> currency: 币别 </summary>
        public string currency { get; set; }

        /// <summary> frozen: 提盈金额 </summary>
        public decimal frozen { get; set; }

        /// <summary> init_money: 初始操盘资金 </summary>
        public decimal init_money { get; set; }

        /// <summary> balance: 帐户余额 </summary>
        public decimal balance { get; set; }

        /// <summary> loan_type: 帐户类型 </summary>
        public string loan_type { get; set; }

        /// <summary> state: 审核结果 </summary>
        public int state { get; set; }

        /// <summary> acccept_by: 同意人员 </summary>
        public string acccept_by { get; set; }

    }
}
