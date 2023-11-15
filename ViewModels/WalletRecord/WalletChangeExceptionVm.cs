namespace NTKServer.ViewModels.WalletRecord
{
	public class WalletChangeExceptionVm
	{
		/// <summary> wallet: 用户编号 </summary>
		public int member_fk { get; set; }

		/// <summary> member: 登入帐号 </summary>
		public string account { get; set; }

		/// <summary> member: 真实姓名 </summary>
		public string real_name { get; set; }

		/// <summary> 更改原因 </summary>
		public string reason { get; set; }

		/// <summary> wallet: 幣別 </summary>
		public string currency { get; set; }

		/// <summary> wallet: 账户金额 </summary>
		public decimal balance { get; set; }

		/// <summary> 錢包增加值 </summary>
		public decimal change_balance { get; set; }

		/// <summary> 折抵券 </summary>
		public decimal coupon { get; set; }

		/// <summary> 折抵券 </summary>
		public decimal change_coupon { get; set; }
	}
}
