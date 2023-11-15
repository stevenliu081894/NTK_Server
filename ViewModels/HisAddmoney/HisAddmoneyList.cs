using NTKServer.Internal;

namespace NTKServer.ViewModels.HisAddmoney
{
    public class HisAddmoneyList
    {
        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> loan_type: 交易号类型 </summary>
        public string loan_type { get; set; }

        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> add_time: 申请时间 </summary>
        public DateTime add_time { get; set; }

        /// <summary> money: 追加金额 </summary>
        public decimal money { get; set; }

        /// <summary> pk: 主键PK </summary>
        public int pk { get; set; }

        /// <summary> status: 审核结果 </summary>
        public int status { get; set; }

        /// <summary> verify_time: 审核时间 </summary>
        public DateTime verify_time { get; set; }

        /// <summary> target_name: 審核者姓名 </summary>
        public string target_name { get; set; }

    }
}
