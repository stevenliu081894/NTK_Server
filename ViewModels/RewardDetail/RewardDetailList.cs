namespace NTKServer.ViewModels.RewardDetail
{
    public class RewardDetailList
    {
        /// <summary> borrow_date: 交易日期 </summary>
        public DateTime borrow_date { get; set; }

        /// <summary> account: 下线会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 下线会员名称 </summary>
        public string nickname { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> generation: 第几代 </summary>
        public int generation { get; set; }

        /// <summary> management_fee: 管理費 </summary>
        public decimal management_fee { get; set; }

        /// <summary> rate: 分潤%數 </summary>
        public decimal rate { get; set; }

        /// <summary> reward: 分潤金额 </summary>
        public decimal reward { get; set; }

        /// <summary> type: 收费类型 </summary>
        public int type { get; set; }

    }
}
