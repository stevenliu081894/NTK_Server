using X.PagedList;

namespace NTKServer.ViewModels.StockUs
{
    public class StockUsVm
    {
        public StockUsFilter filter { get; set; }
        public IPagedList<StockUsList> list { get; set; }
    }
}
