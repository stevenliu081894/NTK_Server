using NTKServer.ViewModels.BorrowPlan;
using NTKServer.ViewModels.MultiLang;
using X.PagedList;

namespace NTKServer.ViewModels.Question
{
    public class QuestionVm
    {
        public QuestionFilter filter { get; set; }
        public IPagedList<QuestionList> list { get; set; }

    }
}
