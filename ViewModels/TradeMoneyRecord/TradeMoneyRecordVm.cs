using X.PagedList;

namespace NTKServer.ViewModels.TradeMoneyRecord
{
    public class TradeMoneyRecordVm
    {
        public TradeMoneyRecordFilter filter { get; set; }
        public IPagedList<TradeMoneyRecordList> list { get; set; }
    }
}
