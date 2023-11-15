using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminRoleDto
    {
        /// <summary> 模块FK </summary>
        public int admin_module_fk { get; set; }
        /// <summary> 角色pk </summary>
        public int pk { get; set; }
        /// <summary> 角色名称 </summary>
        public string name { get; set; }
        /// <summary> 角色描述 </summary>
        public string description { get; set; }
        /// <summary> 1_運營商 2_管理處 </summary>
        public int model { get; set; }
        /// <summary> 菜单权限 </summary>
        public string admin_menu { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
        /// <summary> 不可以刪除 </summary>
        public bool lock_delete { get; set; }
    }
}
