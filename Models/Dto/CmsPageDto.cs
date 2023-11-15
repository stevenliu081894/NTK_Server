using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsPageDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 单页标题 </summary>
        public string title { get; set; }
        /// <summary> 单页内容 </summary>
        public string content { get; set; }
        /// <summary> 关键词 </summary>
        public string keywords { get; set; }
        /// <summary> 页面描述 </summary>
        public string description { get; set; }
        /// <summary> 模板文件 </summary>
        public string template { get; set; }
        /// <summary> 单页封面 </summary>
        public int cover { get; set; }
        /// <summary> 阅读量 </summary>
        public int view { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
    }
}
