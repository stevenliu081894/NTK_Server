using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class Statistics
    {
        /// <summary> 入金申请人数 </summary>
        public int recharge_apply { get; set; }
        /// <summary> 提现申请人数 </summary>
        public int withdraw_apply { get; set; }
        /// <summary> 新合约申请人数 </summary>
        public int borrow { get; set; }
        /// <summary> 追加保证金帐号数 </summary>
        public int borrow_add_money { get; set; }
        /// <summary> 交易号续期申请数 </summary>
        public int borrow_renew { get; set; }
        /// <summary> 扩大融资申请 </summary>
        public int borrow_add_finance { get; set; }
        /// <summary> 提前终止交易申请 </summary>
        public int borrow_stop { get; set; }
        /// <summary> 申请提盈帐号数 </summary>
        public int trade_profit_withdraw { get; set; }


        /// <summary> 交易中帐号数 </summary>
        public int trade_account { get; set; }
        /// <summary> 预警帐号数 </summary>
        public int alert_trade_account { get; set; }
        /// <summary> 今日到期交易号数 </summary>
        public int trade_account_end_today { get; set; }
        /// <summary> 交易中免费融资帐号数 </summary>
        public int trade_account_free_fee { get; set; }



        /// <summary> 交易保证金总额 </summary>
        public int borrow_deposit_money { get; set; }
        /// <summary> 本日合约通过总额 </summary>
        public int borrow_moeny_today { get; set; }
        /// <summary> 本日入金总额 </summary>
        public int recharge_today { get; set; }
        /// <summary> 本日提现总额 </summary>
        public int withdraw_today { get; set; }


        /// <summary> 本日新注册会员数 </summary>
        public int member_register_today { get; set; }
        /// <summary> 审核中会员数 </summary>
        public int member_verifying { get; set; }
        /// <summary> 总会员数 </summary>
        public int total_member { get; set; }
    }
}
