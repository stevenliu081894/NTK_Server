using NTKServer.Internal;

namespace NTKServer.ViewModels.WalletPayment
{
    public class WalletPaymentFilter
    {
        /// <summary> pay_name: 支付名称 </summary>
        [Where("LIKE", "wallet_payment.pay_name")]
        public string? pay_name { get; set; }

        /// <summary> pay_code: 支付代码(识别码) </summary>
        [Where("", "wallet_payment.pay_code")]
        public string? pay_code { get; set; }

    }
}
