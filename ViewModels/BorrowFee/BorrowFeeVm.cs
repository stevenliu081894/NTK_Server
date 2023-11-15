using X.PagedList;

namespace NTKServer.ViewModels.BorrowFee
{
    public class BorrowFeeVm
    {
        public BorrowFeeFilter filter { get; set; }
        public IPagedList<BorrowFeeList> list { get; set; }
    }
}
