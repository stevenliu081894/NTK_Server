using X.PagedList;

namespace NTKServer.ViewModels.Document
{
    public class DocumentVm
    {
        public DocumentFilter filter { get; set; }
        public IPagedList<DocumentList> list { get; set; }
    }
}
