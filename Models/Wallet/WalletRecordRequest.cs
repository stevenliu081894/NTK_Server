namespace NTKServer.Models.Wallet
{
    public class WalletRecordRequest
    {
        public int member_pk {  get; set; }
        public int type { get; set; }
        public int subtype { get; set; }
        public string currency { get; set; }
        public int temp_id { get; set; }
        public decimal affect { get; set; }
        public decimal coupon { get; set; }
        public decimal balance { get; set; }
        public DateTime createtime { get; set; }
        public object[]? list { get; set; }
    }
}
