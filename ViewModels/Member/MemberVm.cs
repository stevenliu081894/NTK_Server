using X.PagedList;

namespace NTKServer.ViewModels.Member
{
    public class MemberVm
    {
        public MemberFilter filter { get; set; }
        public IPagedList<MemberList> list { get; set; }
    }
}
