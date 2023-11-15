using X.PagedList;

namespace NTKServer.ViewModels.StatTradeAccount
{
    public class StatTradeAccountVm
    {
        public StatTradeAccountFilter filter { get; set; }
        public IPagedList<StatTradeAccountList> list { get; set; }
    }
}
