using System;
using DB.Services;
using NTKServer.Cache;
using NTKServer.ViewModels.Home;

namespace NTKServer.Business
{
	public class HomeBiz
	{
        #region ViewModel

        public static HomeVm GetHomeVm()
        {
            //CacheQuery.SelectDB(CacheEnum.admin);
            //return CacheQuery.StringGet<HomeVm>("HomeVm");

            return new HomeVm
            {
                RechargeApplyCount = StatisticsBiz.GetRechargeApplyCount(),
                WithdrawApplyCount = StatisticsBiz.GetWithdrawApplyCount(),
                BorrowCount = StatisticsBiz.GetBorrowCount(),
                BorrowAddMoneyCount = StatisticsBiz.GetBorrowAddMoneyCount(),
                BorrowAddFinanceCount = StatisticsBiz.GetBorrowAddFinanceCount(),
                BorrowStopCount = StatisticsBiz.GetBorrowStopCount(),
                BorrowRenewCount = StatisticsBiz.GetBorrowRenewCount(),
                TradeBenefitWithdrawCount = StatisticsBiz.GetTradeProfitWithdrawCount(),
                TradeAccountCount = StatisticsBiz.GetTradeAccountCount(),
                AlertTradeAccountCount = StatisticsBiz.GetAlertTradeAccountCount(),
                LiquidatedAccountTodayCount = StatisticsBiz.GetLiquidatedAccountTodayCount(),
                TradeAccountEndTodayCount = StatisticsBiz.GetTradeAccountEndTodayCount(),
                BorrowDepositMoney = StatisticsBiz.GetBorrowDepositMoney(),
                BorrowMoneyToday = StatisticsBiz.GetBorrowMoneyToday(),
                RechargeToday = StatisticsBiz.GetRechargeToday(),
                WithdrawToday = StatisticsBiz.GetWithdrawToday(),
                MemberRegisterToday = StatisticsBiz.GetMemberRegisterToday(),
                MemberVerifying = StatisticsBiz.GetMemberVerifying(),
                TotalMember = StatisticsBiz.GetTotalMember(),
                TradeAccountFreeFeeCount = StatisticsBiz.GetTradeAccountFreeFeeCount()
            };
        }

        #endregion	
    }
}

