using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminMenuDto
    {
        /// <summary> 模块 </summary>
        public int admin_module_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 上级菜单 </summary>
        public int parent { get; set; }
        /// <summary> 模块名称 </summary>
        public string module { get; set; }
        /// <summary> 菜单标题 </summary>
        public string title { get; set; }
        /// <summary> 多國語系鍵值 </summary>
        public string title_key { get; set; }
        /// <summary> 菜单图标 </summary>
        public string icon { get; set; }
        /// <summary> 链接类型（link：外链，module：模块） </summary>
        public string url_type { get; set; }
        /// <summary> 链接地址 </summary>
        public string url_value { get; set; }
        /// <summary> 链接打开方式：_blank,_self </summary>
        public string url_target { get; set; }
        /// <summary> 网站上线后是否隐藏 </summary>
        public bool online_hide { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 是否为系统菜单，系统菜单不可删除 </summary>
        public int system_menu { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
        /// <summary> 参数 </summary>
        public string parameter { get; set; }
    }
}
