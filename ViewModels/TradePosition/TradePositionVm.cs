using X.PagedList;

namespace NTKServer.ViewModels.TradePosition
{
    public class TradePositionVm
    {
        public TradePositionFilter filter { get; set; }
        public IPagedList<TradePositionList> list { get; set; }
    }
}
