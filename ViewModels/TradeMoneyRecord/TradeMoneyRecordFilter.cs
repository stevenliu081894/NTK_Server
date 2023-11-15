using NTKServer.Internal;

namespace NTKServer.ViewModels.TradeMoneyRecord
{
    public class TradeMoneyRecordFilter
    {
        /// <summary> sub_account: 交易帳號 </summary>
        [Where("LIKE", "trade_money_record.sub_account")]
        public string? sub_account { get; set; }

        /// <summary> begin_time: 起始時間 </summary>
        [Where(">=", ".begin_time")]
        public DateTime? begin_time { get; set; }

        /// <summary> end_time: 結束時間 </summary>
        [Where("<", ".end_time")]
        public DateTime? end_time { get; set; }

        /// <summary> sn: 流水号 </summary>
        [Where("LIKE", "trade_money_record.sn")]
        public string? sn { get; set; }

        /// <summary> temp_id: 帐务类型 </summary>
        [Where("=", "trade_money_record.temp_id")]
        public int? temp_id { get; set; }

    }
}
