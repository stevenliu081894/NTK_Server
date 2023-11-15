using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsModelDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 模型名称 </summary>
        public string name { get; set; }
        /// <summary> 模型标题 </summary>
        public string title { get; set; }
        /// <summary> 附加表名称 </summary>
        public string table { get; set; }
        /// <summary> 模型类别：0-系统模型，1-普通模型，2-独立模型 </summary>
        public int type { get; set; }
        /// <summary>  </summary>
        public string icon { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 是否系统模型 </summary>
        public int system { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
    }
}
