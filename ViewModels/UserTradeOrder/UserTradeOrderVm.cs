using X.PagedList;

namespace NTKServer.ViewModels.UserTradeOrder
{
    public class UserTradeOrderVm
    {
        public UserTradeOrderFilter filter { get; set; }
        public List<UserTradeOrderList> list { get; set; }
    }
}
