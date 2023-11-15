using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsMenuDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 导航id </summary>
        public int nid { get; set; }
        /// <summary> 父级id </summary>
        public int pid { get; set; }
        /// <summary> 栏目id </summary>
        public int column { get; set; }
        /// <summary> 单页id </summary>
        public int page { get; set; }
        /// <summary> 类型：0-栏目链接，1-单页链接，2-自定义链接 </summary>
        public int type { get; set; }
        /// <summary> 菜单标题 </summary>
        public string title { get; set; }
        /// <summary> 链接 </summary>
        public string url { get; set; }
        /// <summary> css类 </summary>
        public string css { get; set; }
        /// <summary> 链接关系网 </summary>
        public string rel { get; set; }
        /// <summary> 打开方式 </summary>
        public string target { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
    }
}
