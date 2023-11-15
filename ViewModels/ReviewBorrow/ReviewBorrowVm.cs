using X.PagedList;

namespace NTKServer.ViewModels.ReviewBorrow
{
    public class ReviewBorrowVm
    {
        public ReviewBorrowFilter filter { get; set; }
        public IPagedList<ReviewBorrowList> list { get; set; }
    }
}
