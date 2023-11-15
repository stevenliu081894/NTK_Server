using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MutilangSubjectDto
    {
        /// <summary> Key </summary>
        public string lang { get; set; }
        /// <summary> 名称 </summary>
        public string title { get; set; }
        /// <summary> 启用 </summary>
        public int enable { get; set; }
        /// <summary> 图标 </summary>
        public string icon { get; set; }
        /// <summary> 图标檔案 </summary>
        public IFormFile icon_file { get; set; }
        /// <summary> 后端预设语言 </summary>
        public int admin_default { get; set; }
        /// <summary> 用户端预设语言 </summary>
        public int app_default { get; set; }
    }
}
