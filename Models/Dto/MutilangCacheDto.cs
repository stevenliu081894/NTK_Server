using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MutilangCacheDto
    {
        /// <summary> 鍵值 </summary>
        public string key { get; set; }
        /// <summary> 語系 </summary>
        public string lang { get; set; }
        /// <summary> json格式翻譯檔 </summary>
        public string value { get; set; }
    }
}
