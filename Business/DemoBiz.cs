using DB.Services;
using NTKServer.Tool;
using NTKServer.ViewModels.Demo;

namespace NTKServer.Business
{
    public class DemoBiz
    {
        public static List<DemoSearchList> GetSearchList(DemoSearchFilter filter)
        {
            string whereSql = SqlTool.Build(filter);
            return TradeMoneyCheckService.FindDemoSearch(whereSql)?
                .Select(demoSearch => PublicTool.convertUtcToLocalTime(demoSearch)).ToList();
        }
    }
}
