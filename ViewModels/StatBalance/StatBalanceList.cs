namespace NTKServer.ViewModels.StatBalance
{
    public class StatBalanceList
    {
        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 会员名称 </summary>
        public string nickname { get; set; }

        /// <summary> create_time: 时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> name: 类型 </summary>
        public int type { get; set; }
        public string name { get; set; }

        /// <summary> recharge: 充值 </summary>
        public decimal recharge { get; set; }

        /// <summary> withdraw: 提现 </summary>
        public decimal withdraw { get; set; }

    }
}
