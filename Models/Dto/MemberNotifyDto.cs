using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MemberNotifyDto
    {
        /// <summary>  </summary>
        public int member_fk { get; set; }
        /// <summary> 邮箱通知 </summary>
        public int EmailNotify { get; set; }
        /// <summary> 站内信通知 </summary>
        public int SiteMessageNotify { get; set; }
        /// <summary> 账户预警通知 </summary>
        public int AccountAlertNotify { get; set; }
        /// <summary> 账户暴仓通知 </summary>
        public int AccountMarginCallNotify { get; set; }
        /// <summary> 股票成交通知 </summary>
        public int StockTransactionNotify { get; set; }
        /// <summary> 账号到期通知（0=不通知, 1=一天前, 2=三天前, 3=已到期），默认值为2 </summary>
        public int AccountExpiryNotify { get; set; }
        /// <summary> 优惠通知 </summary>
        public int PromotionsNotify { get; set; }
        /// <summary> 入金审核通过通知 </summary>
        public int DepositApprovedNotify { get; set; }
        /// <summary> 出金审核通过通知 </summary>
        public int WithdrawalApprovedNotify { get; set; }
        /// <summary> 交易账号审核通过通知 </summary>
        public int TradingAccountApprovedNotify { get; set; }
    }
}
