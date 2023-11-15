using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsDocumentDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 文章key </summary>
        public string cid { get; set; }
        /// <summary> 语系 </summary>
        public string lang { get; set; }
        /// <summary> 标题 </summary>
        public string title { get; set; }
        /// <summary> 内文 </summary>
        public string content { get; set; }
        /// <summary> 自定义属性 </summary>
        public string flag { get; set; }
        /// <summary> 阅读量 </summary>
        public int view { get; set; }
        /// <summary> 评论数 </summary>
        public int comment { get; set; }
        /// <summary> 点赞数 </summary>
        public int good { get; set; }
        /// <summary> 踩数 </summary>
        public int bad { get; set; }
        /// <summary> 收藏数量 </summary>
        public int mark { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 状态 </summary>
        public bool status { get; set; }
        /// <summary> 回收站 </summary>
        public bool trash { get; set; }
    }
}
