using NTKServer.Internal;

namespace NTKServer.ViewModels.AdminBank
{
    public class AdminBankFilter
    {
        /// <summary> type: 钱包类别 </summary>
        [Where("=", "type")]
        public int? type { get; set; }

        /// <summary> currency: 收款币别 </summary>
        [Where("=", "currency")]
        public string? currency { get; set; }

        /// <summary> card: 银行卡号 </summary>
        [Where("LIKE", "card")]
        public string? card { get; set; }

    }
}
