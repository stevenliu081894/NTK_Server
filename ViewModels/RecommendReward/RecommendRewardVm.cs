using X.PagedList;

namespace NTKServer.ViewModels.RecommendReward
{
    public class RecommendRewardVm
    {
        public RecommendRewardFilter filter { get; set; }
        public IPagedList<RecommendRewardList> list { get; set; }
    }
}
