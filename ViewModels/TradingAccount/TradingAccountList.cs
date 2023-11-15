using NTKServer.Internal;

namespace NTKServer.ViewModels.TradingAccount
{
    public class TradingAccountList
    {
        /// <summary> market: 市场 </summary>
        public string market { get; set; }

        /// <summary> sub_account: 子帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> balance: 总市值 </summary>
        public decimal balance { get; set; }

        /// <summary> status: 状态 </summary>
        public int status { get; set; }

		/// <summary> status: 状态 </summary>
		public string statusText
		{
			get
			{
                return ConvertEnum.ConvertAccountStatus(status);
			}
		}

		/// <summary> loan_type: 合约类型 </summary>
		public string loan_type { get; set; }

        /// <summary> warningline: 预警线 </summary>
        public decimal warningline { get; set; }

        /// <summary> breakline: 平仓线 </summary>
        public decimal breakline { get; set; }

        /// <summary> position_value: 持仓市值 </summary>
        public decimal position_value { get; set; }

        /// <summary> begin_time: 起始时间 </summary>
        public DateTime begin_time { get; set; }

        /// <summary> end_time: 结束时间 </summary>
        public DateTime end_time { get; set; }

        /// <summary> member_fk: 客户 PK </summary>
        public int member_fk { get; set; }

        /// <summary> account: 帐号 </summary>
        public string account { get; set; }

        /// <summary> member_name: 姓名 </summary>
        public string member_name { get; set; }

    }
}
