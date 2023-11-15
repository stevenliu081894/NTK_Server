using X.PagedList;

namespace NTKServer.ViewModels.RewardDetail
{
    public class RewardDetailVm
    {
        public RewardDetailFilter filter { get; set; }
        public IPagedList<RewardDetailList> list { get; set; }
    }
}
