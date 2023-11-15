using X.PagedList;

namespace NTKServer.ViewModels.AdminLog
{
    public class AdminLogVm
    {
        public AdminLogFilter filter { get; set; }
        public IPagedList<AdminLogList> list { get; set; }
    }
}
