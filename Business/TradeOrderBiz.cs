using DB.Services;
using MaxMind.GeoIP2.Responses;
using Models.Dto;
using NTKServer.Tool;
using NTKServer.ViewModels.SysMarket;
using NTKServer.ViewModels.TradeAccount;

namespace NTKServer.Business
{
    public class TradeOrderBiz
    {
        public static List<TradeStopSearchList> GetTradeStopSearchList(TradeStopSearchFilter filter)
        {
            string whereSql = SqlTool.Build(filter);
            return TradeOrderService.FindTradeOrderSearch(whereSql)?
                .Select(tradeStopSearch => PublicTool.convertUtcToLocalTime(tradeStopSearch)).ToList();
        }

        public static List<TradeEntrustedSearchList> GetEntrustedSearchList(TradeEntrustedSearchFilter filter)
        {
            string whereSql = SqlTool.Build(filter);
            return TradeOrderService.FindEntrustedOrderSearch(whereSql);
        }

        public static void CancelOrder(TradeEntrustedVm vm, string ip, string adminName)
        {
            var order = TradeOrderService.Find(Convert.ToInt32(vm.sn));
            switch (order.dir)
            {
                case (int)DirType.Buy:
                    // 1. 想买进(撤单)
                    //  trade_cancel
                    //  trade_order (free_volume[減少]/cancel_volume[增加] 两栏)
                    //  trade_frozen -> 撤销冻结的金额, 扣抵结果 = 0 直接整笔删除
                    //  trade_account -> 冻结金额减少
                    TradeOrderService.UpdateCancelOrderVolume(Convert.ToInt32(vm.sn));
                    TradeFrozenService.CancelFrozenMoney(order);
                    TradeAccountService.UpdateAccountFrozenMoney(order);
                    TradeCancelService.AddCancelOrderRecord(order, ip, adminName);
                    break;
                case (int)DirType.Sell:
                    // 2. 想把股票卖出
                    //  trade_cancel
                    //  trade_order (free_volume[減少]/cancel_volume[增加] 两栏)
                    //  trade_position --> sell_volume 冻结量 - [撤单量]
                    //  trade_frozen -->撤销冻结的量, 扣抵结果 = 0 直接整笔删除
                    TradeOrderService.UpdateCancelOrderVolume(Convert.ToInt32(vm.sn));
                    TradeFrozenService.CancelFrozenVolume(order);
                    TradePositionService.UpdateSellVolume(order);
                    TradeCancelService.AddCancelOrderRecord(order, ip, adminName);
                    break;
            }
        }
    }
}
