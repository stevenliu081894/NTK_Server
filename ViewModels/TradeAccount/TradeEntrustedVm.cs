namespace NTKServer.ViewModels.TradeAccount
{
    public class TradeEntrustedVm
    {
        public string? sn { get; set; }
        public byte dir { get; set; }
        public byte? status { get; set; }
        public byte? order_source { get; set; }
        public string? stock_code { get; set; }
        public string? stock_name { get; set; }
        public decimal? price { get; set; }
        public int volume { get; set; }
        public int? free_volume { get; set; }
        public int succeed_volume { get; set; }
        public DateTime? order_time { get; set; }
        public DateTime? order_time_time_zone { get; set; }
        public string? order_ip { get; set; }
        public string? cancel_by { get; set; }
        public string? market { get; set; }
        public string? sub_account { get; set; }
        /// <summary> isSucceed:儲存結果</summary>
        public bool isSucceed { get; set; }
    }
}
