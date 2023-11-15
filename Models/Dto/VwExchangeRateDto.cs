namespace NTKServer.Models.Dto
{
	public class VwExchangeRateDto
	{
		/// <summary>  </summary>
		public int pk { get; set; }
		/// <summary> 日期 yyyy-MM-dd </summary>
		public string date { get; set; }
		/// <summary> 币别A名称 </summary>
		public string currency_symbol { get; set; }
		/// <summary> 币别B名称 </summary>
		public string base_symbol { get; set; }
		/// <summary> A转B汇率 </summary>
		public decimal inward_rate { get; set; }
		/// <summary> B转A汇率 </summary>
		public decimal outward_rate { get; set; }
		/// <summary> 汇率时间 </summary>
		public DateTime create_time { get; set; }
	}
}
