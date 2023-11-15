using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsDocumentNewsDto
    {
        /// <summary> 文档id </summary>
        public int aid { get; set; }
        /// <summary> 内容 </summary>
        public string content { get; set; }
        /// <summary> 摘要 </summary>
        public string summary { get; set; }
        /// <summary> 新闻首图 </summary>
        public int imgitem { get; set; }
    }
}
