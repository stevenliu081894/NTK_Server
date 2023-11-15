using X.PagedList;

namespace NTKServer.ViewModels.HisAddfinancing
{
    public class HisAddfinancingVm
    {
        public HisAddfinancingFilter filter { get; set; }
        public IPagedList<HisAddfinancingList> list { get; set; }
    }
}
