namespace NTKServer.ViewModels.MultiLang
{
    public class MultiLangLanghList
    {
        /// <summary> 语系 </summary>
        public string lang { get; set; }
        /// <summary>  </summary>
        public string title { get; set; }
        /// <summary>  </summary>
        public sbyte enable { get; set; }
        public string enableTxt
        {
            get
            {
                return enable switch
                {
                    0 => "禁用",
                    1 => "启用"
                };
            }
        }
        /// <summary> 后端预设语言 </summary>
        public bool admin_default { get; set; }
        public string adminDefaultTxt
        {
            get
            {
                return admin_default switch
                {
                    false => "禁用",
                    true => "预设"
                };
            }
        }
        /// <summary> 用户端预设语言 </summary>
        public bool app_default { get; set; }
        public string appDefaultTxt
        {
            get
            {
                return app_default switch
                {
                    false => "禁用",
                    true => "预设"
                };
            }
        }
    }
}
