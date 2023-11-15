namespace NTKServer.Models
{
	public class TokenModel
	{
		public string sub_account { get; set; }
		public int status { get; set; }
		public int member_fk { get; set; }
		public string ip { get; set; }
		public string device { get; set; }
		public string lang { get; set; }
		public string time_zone { get; set; }
		public string market { get; set; } // 交易市场别
	}
}
