using NTKServer.Internal;

namespace NTKServer.ViewModels.Addfinancing
{
    public class AddfinancingReview
    {
        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> trade_status: 交易号状态 </summary>
        public int trade_status { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> loan_type: 帐号类型 </summary>
        public string loan_type { get; set; }

        /// <summary> end_time: 交易号结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> balance: 交易号余额 </summary>
        public decimal balance { get; set; }

        /// <summary> warningline: 预警线 </summary>
        public decimal warningline { get; set; }

        /// <summary> money: 扩大融资金额 </summary>
        public decimal money { get; set; }

        /// <summary> borrow_interest: 支付管理费 </summary>
        public decimal borrow_interest { get; set; }

        /// <summary> freeze: 錢包共支付 </summary>
        public decimal freeze { get; set; }

        /// <summary> last_deposit_money: 原始保证金总额 </summary>
        public decimal last_deposit_money { get; set; }

        /// <summary> member_fk: 会员编号 </summary>
        public int member_fk { get; set; }

        /// <summary> account: 会帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 姓名 </summary>
        public string member_name { get; set; }

        /// <summary> add_time: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> status: 审核状态 </summary>
        public int status { get; set; }

    }
}
