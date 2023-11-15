using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class SysCountryDto
    {
        /// <summary> 主键 </summary>
        public string pk { get; set; }
        /// <summary> 标签 </summary>
        public string label { get; set; }
        /// <summary> 启用 </summary>
        public bool enable { get; set; }
        /// <summary> 使用語言 </summary>
        public string lang { get; set; }
        /// <summary> 使用幣種 </summary>
        public string currency { get; set; }
        /// <summary> 国旗图片url </summary>
        public string flag { get; set; }
        /// <summary> 国旗图片 </summary>
        public IFormFile flag_file { get; set; }
        /// <summary> 电话国码 </summary>
        public string code { get; set; }
    }
}
