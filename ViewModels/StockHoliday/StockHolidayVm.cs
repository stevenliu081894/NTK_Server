using X.PagedList;

namespace NTKServer.ViewModels.StockHoliday
{
    public class StockHolidayVm
    {
        public StockHolidayFilter filter { get; set; }
        public IPagedList<StockHolidayList> list { get; set; }
    }
}
