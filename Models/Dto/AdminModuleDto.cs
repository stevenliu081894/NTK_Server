using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminModuleDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 模块名称（标识） </summary>
        public string name { get; set; }
        /// <summary> 模块标题 </summary>
        public string title { get; set; }
        /// <summary> 图标 </summary>
        public string icon { get; set; }
        /// <summary> 描述 </summary>
        public string description { get; set; }
        /// <summary> 模块唯一标识符 </summary>
        public string identifier { get; set; }
        /// <summary> 1.一般 2.總部專用 </summary>
        public int system_module { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
    }
}
