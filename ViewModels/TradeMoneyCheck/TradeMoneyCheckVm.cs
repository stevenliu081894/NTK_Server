using X.PagedList;

namespace NTKServer.ViewModels.TradeMoneyCheck
{
    public class TradeMoneyCheckVm
    {
        public TradeMoneyCheckFilter filter { get; set; }
        public IPagedList<TradeMoneyCheckList> list { get; set; }
    }
}
