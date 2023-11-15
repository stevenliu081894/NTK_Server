using NTKServer.ViewModels.MultiLang;
using X.PagedList;

namespace NTKServer.ViewModels.Bulletin
{
    public class BulletinVm
    {
        public BulletinFilter filter { get; set; }
        public IPagedList<BulletinList> list { get; set; }
    }
}
