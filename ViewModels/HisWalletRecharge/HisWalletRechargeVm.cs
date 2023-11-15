using X.PagedList;

namespace NTKServer.ViewModels.HisWalletRecharge
{
    public class HisWalletRechargeVm
    {
        public HisWalletRechargeFilter filter { get; set; }
        public IPagedList<HisWalletRechargeList> list { get; set; }
    }
}
