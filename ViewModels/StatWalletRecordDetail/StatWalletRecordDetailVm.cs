using X.PagedList;

namespace NTKServer.ViewModels.StatWalletRecordDetail
{
    public class StatWalletRecordDetailVm
    {
        public StatWalletRecordDetailFilter filter { get; set; }
        public IPagedList<StatWalletRecordDetailList> list { get; set; }
    }
}
