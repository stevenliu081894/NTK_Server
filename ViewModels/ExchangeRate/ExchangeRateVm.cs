using X.PagedList;

namespace NTKServer.ViewModels.ExchangeRate
{
    public class ExchangeRateVm
    {
        public ExchangeRateFilter filter { get; set; }
        public IPagedList<ExchangeRateList> list { get; set; }
    }
}
