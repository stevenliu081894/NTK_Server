using X.PagedList;

namespace NTKServer.ViewModels.CmsSupport
{
    public class CmsSupportVm
    {
        public CmsSupportFilter filter { get; set; }
        public IPagedList<CmsSupportList> list { get; set; }
    }
}
