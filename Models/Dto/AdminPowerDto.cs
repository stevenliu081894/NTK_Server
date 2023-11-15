using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminPowerDto
    {
        /// <summary> 模块 </summary>
        public int admin_module_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 上级菜单 </summary>
        public int parent { get; set; }
        /// <summary> 菜单标题 </summary>
        public string title { get; set; }
        /// <summary> 链接地址 </summary>
        public string url_value { get; set; }
        /// <summary> 菜单图标 </summary>
        public string icon { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 總部專用菜单 </summary>
        public bool system_menu { get; set; }
        /// <summary> 上线后要隐藏 </summary>
        public bool online_hide { get; set; }
        /// <summary> 链接类型（link：外链，module：模块） </summary>
        public string url_type { get; set; }
        /// <summary> 链接打开方式：_blank,_self </summary>
        public string url_target { get; set; }
        /// <summary> 最高权限才能使用 </summary>
        public int admin { get; set; }
        /// <summary> 参数 </summary>
        public string parameter { get; set; }
    }
}
