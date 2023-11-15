namespace NTKServer.ViewModels.StatTradeAccount
{
    public class StatTradeAccountList
    {
        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> loan_type: 融资名称 </summary>
        public string loan_type { get; set; }

        /// <summary> margin: 初始保证金 </summary>
        public decimal margin { get; set; }

        /// <summary> loan_money: 融资总额 </summary>
        public decimal loan_money { get; set; }

        /// <summary> dcount: 帐号数量 </summary>
        public int dcount { get; set; }

    }
}
