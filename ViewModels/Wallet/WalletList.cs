
namespace NTKServer.ViewModels.Wallet
{
    public class WalletList
    {
        /// <summary> member_fk: 用户编号 </summary>
        public int member_fk { get; set; }

        /// <summary> account: 登入帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> currency: 幣別 </summary>
        public string currency { get; set; }

        /// <summary> balance: 账户金额 </summary>
        public decimal balance { get; set; }

        /// <summary> freeze: 冻结金额 </summary>
        public decimal freeze { get; set; }

        /// <summary> status: 钱包被冻结 </summary>
        public bool status { get; set; }

        /// <summary> coupon: 折抵券 </summary>
        public decimal coupon { get; set; }

        /// <summary> total_recharge: 累计充值 </summary>
        public decimal total_recharge { get; set; }

        /// <summary> total_withdraw: 累计提现 </summary>
        public decimal total_withdraw { get; set; }

        /// <summary> last_update_time: 最后更动时间 </summary>
        public DateTime last_update_time { get; set; }

        /// <summary> admin_user_fk: 专属客服 </summary>
        public int admin_user_fk { get; set; }

        /// <summary> id_auth: 身份审核状况 </summary>
        public int id_auth { get; set; }

        /// <summary> last_login_time: 最后一次登录时间 </summary>
        public DateTime last_login_time { get; set; }

        /// <summary> last_login_ip: 最后一次登录ip </summary>
        public string last_login_ip { get; set; }

    }
}
