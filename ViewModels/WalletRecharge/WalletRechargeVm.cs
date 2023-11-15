using X.PagedList;

namespace NTKServer.ViewModels.WalletRecharge
{
    public class WalletRechargeVm
    {
        public WalletRechargeFilter filter { get; set; }
        public IPagedList<WalletRechargeList> list { get; set; }
    }
}
