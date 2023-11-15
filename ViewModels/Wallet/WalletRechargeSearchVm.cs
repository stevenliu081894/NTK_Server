using X.PagedList;

namespace NTKServer.ViewModels.Wallet
{
	public class WalletRechargeSearchVm
	{
		/// <summary> 搜寻条件 </summary>
		public WalletRechargeSearchFilter filter { get; set; }

		/// <summary> 资料结果 </summary>
		public IPagedList<WalletRechargeSearchList> list { get; set; }

		/// <summary> 分页 </summary>
		public int page { get; set; } = 1;

		/// <summary> 排序栏位名称 </summary>
		public string sort { get; set; }
	}

	public class WalletRechargeSearchFilter
	{
		//[Where("=", "code")]
		//public string? code { get; set; }
	}

	public class WalletRechargeSearchList
	{ 
	}
}
