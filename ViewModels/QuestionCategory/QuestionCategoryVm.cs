using NTKServer.ViewModels.MultiLang;
using X.PagedList;

namespace NTKServer.ViewModels.QuestionCategory
{
    public class QuestionCategoryVm
    {
        public QuestionCategoryFilter filter { get; set; }
        public IPagedList<QuestionCategoryList> list { get; set; }
    }
}
