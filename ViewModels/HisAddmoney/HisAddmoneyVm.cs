using X.PagedList;

namespace NTKServer.ViewModels.HisAddmoney
{
    public class HisAddmoneyVm
    {
        public HisAddmoneyFilter filter { get; set; }
        public IPagedList<HisAddmoneyList> list { get; set; }
    }
}
