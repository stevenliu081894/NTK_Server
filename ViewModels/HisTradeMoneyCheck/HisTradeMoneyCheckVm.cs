using X.PagedList;

namespace NTKServer.ViewModels.HisTradeMoneyCheck
{
    public class HisTradeMoneyCheckVm
    {
        public HisTradeMoneyCheckFilter filter { get; set; }
        public IPagedList<HisTradeMoneyCheckList> list { get; set; }
    }
}
