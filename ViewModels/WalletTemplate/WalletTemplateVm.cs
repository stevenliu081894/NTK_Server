using X.PagedList;

namespace NTKServer.ViewModels.WalletTemplate
{
    public class WalletTemplateVm
    {
        public WalletTemplateFilter filter { get; set; }
        public IPagedList<WalletTemplateList> list { get; set; }
    }
}
