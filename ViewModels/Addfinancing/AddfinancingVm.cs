using X.PagedList;

namespace NTKServer.ViewModels.Addfinancing
{
    public class AddfinancingVm
    {
        public AddfinancingFilter filter { get; set; }
        public IPagedList<AddfinancingList> list { get; set; }
    }
}
