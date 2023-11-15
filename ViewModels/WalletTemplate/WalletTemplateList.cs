namespace NTKServer.ViewModels.WalletTemplate
{
    public class WalletTemplateList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> temp_id: 模板id </summary>
        public int temp_id { get; set; }

        /// <summary> lang: 语系 </summary>
        public string lang { get; set; }

        /// <summary> name: 说明 </summary>
        public string name { get; set; }

        /// <summary> template: 模板 </summary>
        public string template { get; set; }

        /// <summary> param: 参数说明 </summary>
        public string param { get; set; }

        /// <summary> demo: 范例 </summary>
        public string demo { get; set; }

    }
}
