using X.PagedList;

namespace NTKServer.ViewModels.Banner
{
    public class BannerVm
    {
        public BannerFilter filter { get; set; }
        public IPagedList<BannerList> list { get; set; }
    }
}
