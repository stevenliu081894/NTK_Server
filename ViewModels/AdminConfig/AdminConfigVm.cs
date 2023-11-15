using X.PagedList;

namespace NTKServer.ViewModels.AdminConfig
{
    public class AdminConfigVm
    {
        public AdminConfigFilter filter { get; set; }
        public IPagedList<AdminConfigList> list { get; set; }
    }
}
