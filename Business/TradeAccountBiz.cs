using DB.Services;
using NTKServer.Tool;
using NTKServer.ViewModels.TradeAccount;

namespace NTKServer.Business
{
    public class TradeAccountBiz
    {
        public static List<TradeAccountSearchList> GetSearchList(TradeAccountSearchFilter filter)
        {
            string whereSql = SqlTool.Build(filter);
            return TradeAccountService.FindTradeAccountSearch(whereSql)?
                .Select(tradeAccountSearch => PublicTool.convertUtcToLocalTime(tradeAccountSearch)).ToList();
        }
    }
}
