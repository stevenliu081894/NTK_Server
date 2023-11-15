using NTKServer.Internal;

namespace NTKServer.ViewModels.TradeMoneyRecord
{
    public class TradeMoneyRecordList
    {
        public int pk { get; set; }
        /// <summary> create_datetime: 时间 </summary>
        public DateTime create_datetime { get; set; }

        /// <summary> sub_account: 交易帐号 </summary>
        public string sub_account { get; set; }

        /// <summary> sn: 流水号 </summary>
        public string sn { get; set; }

        /// <summary> temp_id: 帐务类型 </summary>
        public int temp_id { get; set; }

		/// <summary> temp_name: 帐务类型 </summary>
		public string temp_name { get; set; }

        /// <summary> info: 说明 </summary>
        public string info { get; set; }

        /// <summary> affect: 異動金额 </summary>
        public decimal affect { get; set; }

        /// <summary> exchange: 汇率 </summary>
        public decimal exchange { get; set; }

        /// <summary> wallet_amount: 主钱包異動 </summary>
        public decimal wallet_amount { get; set; }

        /// <summary> balance: 钱包余额 </summary>
        public decimal balance { get; set; }

    }
}
