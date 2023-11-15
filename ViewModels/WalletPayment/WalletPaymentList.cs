namespace NTKServer.ViewModels.WalletPayment
{
    public class WalletPaymentList
    {
        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> status: 启用 </summary>
        public bool status { get; set; }

        /// <summary> pay_name: 支付名称 </summary>
        public string pay_name { get; set; }

        /// <summary> pay_code: 支付代码(识别码) </summary>
        public string pay_code { get; set; }

        /// <summary> viplists: 可见会员 </summary>
        public string viplists { get; set; }

    }
}
