using X.PagedList;

namespace NTKServer.ViewModels.HisBorrow
{
    public class HisBorrowVm
    {
        public HisBorrowFilter filter { get; set; }
        public IPagedList<HisBorrowList> list { get; set; }
    }
}
