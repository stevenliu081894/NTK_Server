using X.PagedList;

namespace NTKServer.ViewModels.Promotion
{
    public class PromotionVm
    {
        public PromotionFilter filter { get; set; }
        public IPagedList<PromotionList> list { get; set; }
    }
}
