using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.Addfinancing;

namespace NTKServer.Business
{
    public class StatisticsBiz
    {
        #region CRUD
        // 申请请求

        // 入金申请人数
		public static int GetRechargeApplyCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 0");
            return StatisticsService.Count("wallet_recharge", whereSql);
        }

        // 提现申请人数
		public static int GetWithdrawApplyCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 0");
            return StatisticsService.Count("wallet_withdraw", whereSql);
        }

        // 新合约申请人数
		public static int GetBorrowCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("borrow.status = -1").Must("borrow_fee.type = 1");
            return StatisticsService.Count("borrow", "borrow_fee", "borrow.pk = borrow_fee.borrow_fk", whereSql);
        }

        // 追加保证金帐号数
		public static int GetBorrowAddMoneyCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 0");
            return StatisticsService.Count("borrow_addmoney", whereSql);
        }

        // 交易号续期申请数
		public static int GetBorrowRenewCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("type = 1").Must("status = 0");
            return StatisticsService.Count("borrow_request", whereSql);
        }

        // 扩大融资申请
		public static int GetBorrowAddFinanceCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 0");
            return StatisticsService.Count("borrow_addfinancing", whereSql);
        }

        // 提前终止交易申请
		public static int GetBorrowStopCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("type = 2").Must("status = 0");
            return StatisticsService.Count("borrow_request", whereSql);
        }

        // 申请提盈帐号数
		public static int GetTradeProfitWithdrawCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("type = 1").Must("state = 0");
            return StatisticsService.Count("trade_money_check", whereSql);
        }

        // 合约帐号

        // 交易中帐号数
		public static int GetTradeAccountCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 0");
            return StatisticsService.Count("trade_account", whereSql);
        }

        // 预警帐号数
		public static int GetAlertTradeAccountCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 1");
            return StatisticsService.Count("trade_account", whereSql);
        }

        // 今日到期交易号数
		public static int GetTradeAccountEndTodayCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("date(end_time) = utc_date()");
            return StatisticsService.Count("trade_account", whereSql);
        }

        // 今日被平仓帐号数
        public static int GetLiquidatedAccountTodayCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter { })
                .Must("date(notice_close) = utc_date()")
                .Must("status = 2")
                .Must("balance <= breakline");
            return StatisticsService.Count("vw_trade_account", whereSql);
        }

        // 交易中免费融资帐号数
        public static int GetTradeAccountFreeFeeCount()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("trade_account.status = 0").Must("borrow_plan.borrow_type = 'free'");
            return StatisticsService.Count("trade_account", "borrow_plan", "trade_account.borrow_plan_fk = borrow_plan.pk", whereSql);
        }


        // 资金统计

        // 交易保证金总额
		public static int GetBorrowDepositMoney()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{});
            return StatisticsService.Sum("borrow", "deposit_money", whereSql);
        }

        // 本日合约通过总额
		public static int GetBorrowMoneyToday()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 1").Must("date(verify_time) = utc_date()");
            return StatisticsService.Sum("borrow", "deposit_money", whereSql);
        }

        // 本日入金总额
		public static int GetRechargeToday()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 1").Must("date(verify_time) = utc_date()");
            return StatisticsService.Sum("wallet_recharge", "money", whereSql);
        }

        // 本日提现总额
		public static int GetWithdrawToday()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("status = 1").Must("date(verify_time) = utc_date()");
            return StatisticsService.Sum("wallet_withdraw", "money", whereSql);
        }

        // 会员统计

        // 本日新注册会员数
		public static int GetMemberRegisterToday()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("date(create_time) = utc_date()");
            return StatisticsService.Count("member", whereSql);
        }

        // 审核中会员数
		public static int GetMemberVerifying()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{}).Must("id_auth = 3");
            return StatisticsService.Count("member", whereSql);
        }

        // 总会员数
		public static int GetTotalMember()
        {
            string whereSql = SqlTool.Build(new StatisticsFilter{});
            return StatisticsService.Count("member", whereSql);
        }
		#endregion		
	}
}
