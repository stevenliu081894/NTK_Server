using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsColumnDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 父级id </summary>
        public int pid { get; set; }
        /// <summary> 栏目名称 </summary>
        public string name { get; set; }
        /// <summary> 文档模型id </summary>
        public int model { get; set; }
        /// <summary> 链接 </summary>
        public string url { get; set; }
        /// <summary> 链接打开方式 </summary>
        public string target { get; set; }
        /// <summary> 内容 </summary>
        public string content { get; set; }
        /// <summary> 字体图标 </summary>
        public string icon { get; set; }
        /// <summary> 封面模板 </summary>
        public string index_template { get; set; }
        /// <summary> 列表页模板 </summary>
        public string list_template { get; set; }
        /// <summary> 详情页模板 </summary>
        public string detail_template { get; set; }
        /// <summary> 投稿权限 </summary>
        public int post_auth { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
        /// <summary> 是否隐藏 </summary>
        public int hide { get; set; }
        /// <summary> 浏览权限，-1待审核，0为开放浏览，大于0则为对应的用户角色id </summary>
        public int rank_auth { get; set; }
        /// <summary> 栏目属性：0-最终列表栏目，1-外部链接，2-频道封面 </summary>
        public int type { get; set; }
    }
}
