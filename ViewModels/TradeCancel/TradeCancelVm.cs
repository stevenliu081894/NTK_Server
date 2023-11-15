using X.PagedList;

namespace NTKServer.ViewModels.TradeCancel
{
    public class TradeCancelVm
    {
        public TradeCancelFilter filter { get; set; }
        public List<TradeCancelList> list { get; set; }
    }
}
