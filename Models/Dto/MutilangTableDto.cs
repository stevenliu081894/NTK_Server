using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MutilangTableDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 表名稱 </summary>
        public string dbtable { get; set; }
        /// <summary> 欄位名稱 </summary>
        public string field { get; set; }
        /// <summary> 对应pk </summary>
        public string key { get; set; }
        /// <summary> 語言 </summary>
        public string lang { get; set; }
        /// <summary> 翻譯結果 </summary>
        public string value { get; set; }
    }
}
