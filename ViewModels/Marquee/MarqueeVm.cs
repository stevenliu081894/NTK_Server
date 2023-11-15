using X.PagedList;

namespace NTKServer.ViewModels.Marquee
{
    public class MarqueeVm
    {
        public MarqueeFilter filter { get; set; }
        public List<MarqueeList> list { get; set; }
    }
}
