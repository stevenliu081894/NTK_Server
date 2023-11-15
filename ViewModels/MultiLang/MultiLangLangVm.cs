using Microsoft.AspNetCore.Mvc;

namespace NTKServer.ViewModels.MultiLang
{
    public class MultiLangLangVm
    {
        /// <summary>  </summary>
        public string lang { get; set; }
        /// <summary>  </summary>
        public string title { get; set; }
        /// <summary>  </summary>
        [BindProperty]
        public int enable { get; set; }
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

        public string[] enableOption = new[] { "禁用", "启用" };
        /// <summary> 后端预设语言 </summary>
        public int admin_default { get; set; }
        /// <summary> 用户端预设语言 </summary>
        public int app_default { get; set; }
        public bool isSucceed { get; set; }
    }
}
