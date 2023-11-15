using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.HisTradeMoneyCheck;
using NTKServer.ViewModels.TradeMoneyCheck;

namespace NTKServer.Business
{
    public class HisTradeMoneyCheckBiz
    {
		public static List<HisTradeMoneyCheckList> GetHisTradeMoneyCheckList(HisTradeMoneyCheckFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("trade_money_check.type = 0")
				.Must("trade_money_check.state > 0");
			return TradeMoneyCheckService.FindHisTradeMoneyCheckList(whereSql);
        }

        public static TradeMoneyCheckReview GetReview(int pk)
        {
            return TradeMoneyCheckService.FindTradeMoneyCheckReview(pk);
        }
	}
}
