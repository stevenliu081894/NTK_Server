using X.PagedList;

namespace NTKServer.ViewModels.MemberTask
{
    public class MemberTaskVm
    {
        public MemberTaskFilter filter { get; set; }
        public IPagedList<MemberTaskList> list { get; set; }
    }
}
