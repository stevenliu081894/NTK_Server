using X.PagedList;

namespace NTKServer.ViewModels.CmsPopinfo
{
    public class CmsPopinfoVm
    {
        public PopInfoSwitchVM popInfo {  get; set; }
        public CmsPopinfoFilter filter { get; set; }
        public IPagedList<CmsPopinfoList> list { get; set; }
    }
}
