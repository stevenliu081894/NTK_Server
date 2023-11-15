using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MutilangDto
    {
        /// <summary> pk </summary>
        public string key { get; set; }
        /// <summary> 1.common,2.admin,3.app,4.table,5.other </summary>
        public string application { get; set; }
        /// <summary> 模塊 </summary>
        public string module { get; set; }
        /// <summary> 說明 </summary>
        public string description { get; set; }
        /// <summary> 路徑 </summary>
        public string path { get; set; }
        /// <summary> json模板 </summary>
        public string template { get; set; }
    }
}
