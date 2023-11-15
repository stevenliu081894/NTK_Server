
namespace NTKServer.ViewModels.Wallet
{
    public class WalletEditVm
    {
        /// <summary> wallet: 用户编号 </summary>
        public int member_fk { get; set; }

        /// <summary> member: 登入帐号 </summary>
        public string account { get; set; }

        /// <summary> member: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> member: 真实姓名 </summary>
        public string real_name { get; set; }

        /// <summary> wallet: 幣別 </summary>
        public string currency { get; set; }

        /// <summary> wallet: 账户金额 </summary>
        public decimal balance { get; set; }

        /// <summary> wallet: 冻结金额 </summary>
        public decimal freeze { get; set; }

        /// <summary> wallet: 保证金总额 </summary>
        public decimal margin { get; set; }

        /// <summary> wallet: 财富扑满 </summary>
        public decimal richbox_balance { get; set; }

        /// <summary> wallet: 钱包被冻结 </summary>
        public bool status { get; set; }

        /// <summary> wallet: 折抵券 </summary>
        public decimal coupon { get; set; }

        /// <summary> wallet: 累计充值 </summary>
        public decimal total_recharge { get; set; }

        /// <summary> wallet: 累计提现 </summary>
        public decimal total_withdraw { get; set; }

        /// <summary> wallet: 最后更动时间 </summary>
        public DateTime last_update_time { get; set; }

        /// <summary> member: 专属客服 </summary>
        public int admin_user_fk { get; set; }

        /// <summary> member: 电子信箱 </summary>
        public string email { get; set; }

        /// <summary> member: 身份审核状况 </summary>
        public int id_auth { get; set; }

        /// <summary> member: 注册时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> member: 注册IP </summary>
        public string create_ip { get; set; }

        /// <summary> member: 最后一次登录时间 </summary>
        public DateTime last_login_time { get; set; }

        /// <summary> member: 最后一次登录ip </summary>
        public string last_login_ip { get; set; }

        /// <summary> member: 实名认证申请时间 </summary>
        public DateTime auth_time { get; set; }

    }
}
