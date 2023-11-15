using NTKServer.Internal;

namespace NTKServer.ViewModels.RequestStop
{
    public class RequestStopReview
    {
        /// <summary> add_time: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> loan_type: 帐号类型 </summary>
        public string loan_type { get; set; }

        /// <summary> init_money: 初始资金 </summary>
        public decimal init_money { get; set; }

        /// <summary> balance: 帐户余额 </summary>
        public decimal balance { get; set; }

        /// <summary> end_time: 应结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> member_fk: 会员FK </summary>
        public int member_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> pk: 主键PK </summary>
        public int pk { get; set; }

        /// <summary> warningline: 预警线 </summary>
        public decimal warningline { get; set; }

        /// <summary> breakline: 平仓线 </summary>
        public decimal breakline { get; set; }

        /// <summary> status: 审核状态 0_审核中 1_审核通过  2_审核不通过 </summary>
        public int status { get; set; }

        /// <summary> verify_time: 审核时间 </summary>
        public DateTime verify_time { get; set; }

    }
}
