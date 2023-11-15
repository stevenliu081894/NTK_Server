
namespace NTKServer.ViewModels.BorrowAddmoney
{
    public class BorrowAddmoneyReview
    {
        /// <summary> add_time: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> money: 追加金额 </summary>
        public decimal money { get; set; }

        /// <summary> currency: 幣別 </summary>
        public string currency { get; set; }

        /// <summary> trade_status: 帐号状态 </summary>
        public int trade_status { get; set; }

        /// <summary> end_time: 应结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> init_money: 初始资金 </summary>
        public decimal init_money { get; set; }

        /// <summary> balance: 帐户余额 </summary>
        public decimal balance { get; set; }

        /// <summary> warningline: 预警线 </summary>
        public decimal warningline { get; set; }

        /// <summary> breakline: 平仓线 </summary>
        public decimal breakline { get; set; }

        /// <summary> loan_type: 融资名称 </summary>
        public string loan_type { get; set; }

        /// <summary> member_fk: 会员编号 </summary>
        public int member_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> pk: 主键PK </summary>
        public int pk { get; set; }

        /// <summary> exchange: 匯率 </summary>
        public decimal exchange { get; set; }

        /// <summary> freeze: 主錢包提领 </summary>
        public decimal freeze { get; set; }

        /// <summary> status: 状态 </summary>
        public int status { get; set; }

        /// <summary> verify_time: 审核时间 </summary>
        public DateTime verify_time { get; set; }

        /// <summary> target_uid: 審核者ID </summary>
        public int target_uid { get; set; }

        /// <summary> target_name: 審核者姓名 </summary>
        public string target_name { get; set; }

    }
}
