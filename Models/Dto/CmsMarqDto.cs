using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsMarqDto
    {
        /// <summary> 編號 </summary>
        public int pk { get; set; }
        /// <summary> 語言 </summary>
        public string lang { get; set; }
        /// <summary> 啟用 </summary>
        public bool enable { get; set; }
        /// <summary> 輪播內容 </summary>
        public string msg { get; set; }
    }
}
