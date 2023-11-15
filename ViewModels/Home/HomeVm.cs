using X.PagedList;

namespace NTKServer.ViewModels.Home
{
    public class HomeVm
    {
        public int RechargeApplyCount { get; set; }
        public int WithdrawApplyCount { get; set; }
        public int BorrowCount { get; set; }
        public int BorrowAddMoneyCount { get; set; }
        public int BorrowAddFinanceCount { get; set; }
        public int BorrowStopCount { get; set; }
        public int TradeBenefitWithdrawCount { get; set; }
        public int TradeAccountCount { get; set; }
        public int BorrowRenewCount { get; set; }
        public int AlertTradeAccountCount { get; set; }
        public int TradeAccountEndTodayCount { get; set; }
        public int LiquidatedAccountTodayCount { get; set; }
        public int BorrowDepositMoney { get; set; }
        public int BorrowMoneyToday { get; set; }
        public int RechargeToday { get; set; }
        public int WithdrawToday { get; set; }
        public int MemberRegisterToday { get; set; }
        public int MemberVerifying { get; set; }
        public int TotalMember { get; set; }
        public int TradeAccountFreeFeeCount { get; set; }
    }
}
