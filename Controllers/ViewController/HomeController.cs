using Microsoft.AspNetCore.Mvc;
using NTKServer.Business;
using NTKServer.Filter;
using NTKServer.ViewModels.Home;
using NTKServer.ViewModels.Menu;

namespace NTKServer.Controllers;


public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(bool hasPermission = true)
    {
        if (!hasPermission)
        {
            ShowWarning("You don't have permission");
        }
        HomeVm model = new HomeVm
        {
            RechargeApplyCount = 0,
            WithdrawApplyCount = 0,
            BorrowCount = 0,
            BorrowAddMoneyCount = 0,
            BorrowAddFinanceCount = 0,
            BorrowStopCount = 0,
            TradeBenefitWithdrawCount = 0,
            TradeAccountCount = 0,
            BorrowRenewCount = 0,
            AlertTradeAccountCount = 0,
            TradeAccountEndTodayCount = 0,
            LiquidatedAccountTodayCount = 0,
            BorrowDepositMoney = 0,
            BorrowMoneyToday = 0,
            RechargeToday = 0,
            WithdrawToday = 0,
            MemberRegisterToday = 0,
            MemberVerifying = 0,
            TotalMember = 0,
            TradeAccountFreeFeeCount = 0
        };
        return View(model);
    }

}
