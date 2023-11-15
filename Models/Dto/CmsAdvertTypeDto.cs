using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsAdvertTypeDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 分类名称 </summary>
        public string name { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
    }
}
