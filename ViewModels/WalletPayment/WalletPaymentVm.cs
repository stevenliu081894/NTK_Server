using X.PagedList;

namespace NTKServer.ViewModels.WalletPayment
{
    public class WalletPaymentVm
    {
        public WalletPaymentFilter filter { get; set; }
        public IPagedList<WalletPaymentList> list { get; set; }
    }
}
