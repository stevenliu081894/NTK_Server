namespace NTKServer.ViewModels.SysCountry
{
    public class SysCountryList
    {
        /// <summary> pk: 主键 </summary>
        public string pk { get; set; }

        /// <summary> label: 标签 </summary>
        public string label { get; set; }

        /// <summary> enable: 启用 </summary>
        public bool enable { get; set; }

        /// <summary> lang: 語言 </summary>
        public string lang { get; set; }

        /// <summary> currency: 幣種 </summary>
        public string currency { get; set; }

        /// <summary> flag: 国旗 </summary>
        public string flag { get; set; }

        /// <summary> code: 国际码 </summary>
        public string code { get; set; }

    }
}
