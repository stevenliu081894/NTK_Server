using NTKServer.Internal;

namespace NTKServer.ViewModels.WalletWithdraw
{
    public class WalletWithdrawList
    {
        /// <summary> create_time: 申请时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> order_no: 申请编号 </summary>
        public string order_no { get; set; }

        /// <summary> wallet_amount: 金额 </summary>
        public decimal wallet_amount { get; set; }

        /// <summary> exchange: 转换汇率 </summary>
        public decimal exchange { get; set; }

        /// <summary> currency: 提现币别 </summary>
        public string currency { get; set; }

        /// <summary> money: 提现金额 </summary>
        public decimal money { get; set; }

        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> bank: 收款银行 </summary>
        public string bank { get; set; }

        /// <summary> branch: 收款分行 </summary>
        public string branch { get; set; }

        /// <summary> card: 收款卡號 </summary>
        public string card { get; set; }

        /// <summary> bank_account: 卡号持有人 </summary>
        public string bank_account { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 会员名称 </summary>
        public string nickname { get; set; }

    }
}
