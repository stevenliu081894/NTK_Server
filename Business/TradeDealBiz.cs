using DB.Services;
using NTKServer.Tool;
using NTKServer.ViewModels.TradeAccount;

namespace NTKServer.Business
{
    public class TradeDealBiz
    {
        public static List<DealSearchList> GetTradeDealSearchList(DealSearchFilter filter)
        {
            string whereSql = SqlTool.Build(filter);
            return TradeDealService.FindTradeDealSearch(whereSql);
        }
    }
}
