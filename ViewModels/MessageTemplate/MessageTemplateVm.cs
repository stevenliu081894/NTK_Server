using X.PagedList;

namespace NTKServer.ViewModels.MessageTemplate
{
    public class MessageTemplateVm
    {
        public MessageTemplateFilter filter { get; set; }
        public IPagedList<MessageTemplateList> list { get; set; }
    }
}
