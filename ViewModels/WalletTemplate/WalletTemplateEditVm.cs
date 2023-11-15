namespace NTKServer.ViewModels.WalletTemplate
{
    public class WalletTemplateEditVm
    {
        /// <summary> wallet_template: # </summary>
        public int pk { get; set; }

        /// <summary> wallet_template: 模板id </summary>
        public int temp_id { get; set; }

        /// <summary> wallet_template: 语系 </summary>
        public string lang { get; set; }

        /// <summary> wallet_template: 说明 </summary>
        public string name { get; set; }

        /// <summary> wallet_template: 模板 </summary>
        public string template { get; set; }

        /// <summary> wallet_template: 参数说明 </summary>
        public string param { get; set; }

        /// <summary> wallet_template: 范例 </summary>
        public string demo { get; set; }

    }
}
