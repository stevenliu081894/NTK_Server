using NTKServer.Internal;

namespace NTKServer.ViewModels.WalletTemplate
{
    public class WalletTemplateFilter
    {
        /// <summary> temp_id: 模板id </summary>
        [Where("=", "wallet_template.temp_id")]
        public int? temp_id { get; set; }

        /// <summary> lang: 语系 </summary>
        [Where("=", "wallet_template.lang")]
        public string? lang { get; set; }

    }
}
