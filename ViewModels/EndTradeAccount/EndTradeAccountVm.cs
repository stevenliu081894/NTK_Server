using X.PagedList;

namespace NTKServer.ViewModels.EndTradeAccount
{
    public class EndTradeAccountVm
    {
        public EndTradeAccountFilter filter { get; set; }
        public IPagedList<EndTradeAccountList> list { get; set; }
    }
}
