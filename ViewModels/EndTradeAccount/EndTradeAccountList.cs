using NTKServer.Internal;

namespace NTKServer.ViewModels.EndTradeAccount
{
    public class EndTradeAccountList
    {
        /// <summary> sub_account: 子帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> loan_type: 帳號類型 </summary>
        public string loan_type { get; set; }

        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> init_money: 初始資金 </summary>
        public decimal init_money { get; set; }

        /// <summary> balance: 剩餘資金 </summary>
        public decimal balance { get; set; }

        /// <summary> margin: 初始保证金 </summary>
        public decimal margin { get; set; }

        /// <summary> margin_float: 剩餘保证金 </summary>
        public decimal margin_float { get; set; }

        /// <summary> begin_time: 起始时间 </summary>
        public DateTime begin_time { get; set; }

        /// <summary> end_time: 应结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> close_time: 关闭帐号时间 </summary>
        public DateTime close_time { get; set; }

        /// <summary> breakline: 平仓线 </summary>
        public decimal breakline { get; set; }

        /// <summary> member_fk: 客户編號 </summary>
        public int member_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 会员姓名 </summary>
        public string member_name { get; set; }

    }
}
