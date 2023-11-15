using NTKServer.Internal;

namespace NTKServer.ViewModels.WalletRecharge
{
    public class WalletRechargeReview
    {
        /// <summary> create_time: 提交时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> card: 收款卡号 </summary>
        public string card { get; set; }

        /// <summary> bank_name: 收款银行 </summary>
        public string bank_name { get; set; }

        /// <summary> payee: 收款人 </summary>
        public string payee { get; set; }

        /// <summary> order_no: 充值單號 </summary>
        public string order_no { get; set; }

        /// <summary> line_bank: 充值信息 </summary>
        public string line_bank { get; set; }

        /// <summary> pk: 編號 </summary>
        public int pk { get; set; }

        /// <summary> type: 充值方式 </summary>
        public string type { get; set; }

        /// <summary> currency: 币别 </summary>
        public string currency { get; set; }

        /// <summary> money: 充值金额 </summary>
        public decimal money { get; set; }

        /// <summary> exchange: 转换汇率 </summary>
        public decimal exchange { get; set; }

        /// <summary> wallet_amount: 主钱包金额 </summary>
        public decimal wallet_amount { get; set; }

        /// <summary> member_fk: 用户编号 </summary>
        public int member_fk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> create_ip: ip </summary>
        public string create_ip { get; set; }

        /// <summary> form_name: 转账人 </summary>
        public string form_name { get; set; }

        /// <summary> reject_result: 審核失敗原因 </summary>
        public string reject_result { get; set; }

        /// <summary> admin_user_fk: 所属客服 </summary>
        public int admin_user_fk { get; set; }

        /// <summary> id_auth: 會員驗證 </summary>
        public int id_auth { get; set; }

        /// <summary> level_id: 会员等级 </summary>
        public int level_id { get; set; }

    }
}
