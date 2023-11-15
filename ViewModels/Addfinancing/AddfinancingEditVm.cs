using NTKServer.Internal;

namespace NTKServer.ViewModels.Addfinancing
{
    public class AddfinancingEditVm
    {
        /// <summary> borrow_addfinancing: 编号 </summary>
        public int pk { get; set; }

        /// <summary> vw_trade_account: 交易号状态 </summary>
        public int trade_status { get; set; }

        /// <summary> borrow_addfinancing: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> vw_trade_account: 帐号类型 </summary>
        public string loan_type { get; set; }

        /// <summary> vw_trade_account: 交易号结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> vw_trade_account: 市场 </summary>
        public string market { get; set; }

        /// <summary> vw_trade_account: 交易号余额 </summary>
        public decimal balance { get; set; }

        /// <summary> vw_trade_account: 预警线 </summary>
        public decimal warningline { get; set; }

        /// <summary> borrow_addfinancing: 扩大融资金额 </summary>
        public decimal money { get; set; }

        /// <summary> borrow_addfinancing: 支付管理费 </summary>
        public decimal borrow_interest { get; set; }

        /// <summary> borrow_addfinancing: 錢包共支付 </summary>
        public decimal freeze { get; set; }

        /// <summary> borrow_addfinancing: 轉換匯率 </summary>
        public decimal exchange { get; set; }

        /// <summary> borrow_addfinancing: 原始保证金总额 </summary>
        public decimal last_deposit_money { get; set; }

        /// <summary> vw_trade_account: 会员编号 </summary>
        public int member_fk { get; set; }

        /// <summary> vw_trade_account: 会帐号 </summary>
        public string account { get; set; }

        /// <summary> vw_trade_account: 姓名 </summary>
        public string member_name { get; set; }

        /// <summary> borrow_addfinancing: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> borrow_addfinancing: 审核状态 </summary>
        public int status { get; set; }

        /// <summary> borrow_addfinancing: 审核时间 </summary>
        public DateTime verify_time { get; set; }

        /// <summary> borrow_addfinancing: 操作者ID </summary>
        public int target_uid { get; set; }

        /// <summary> borrow_addfinancing: 操作者姓名 </summary>
        public string target_name { get; set; }

    }
}
