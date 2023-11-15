using X.PagedList;

namespace NTKServer.ViewModels.RecommendRegister
{
    public class RecommendRegisterVm
    {
        public RecommendRegisterFilter filter { get; set; }
        public IPagedList<RecommendRegisterList> list { get; set; }
    }
}
