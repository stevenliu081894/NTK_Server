using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminActionDto
    {
        /// <summary> 对应权限id </summary>
        public int admin_menu_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 语言 </summary>
        public string lang { get; set; }
        /// <summary> 行为标题 </summary>
        public string title { get; set; }
        /// <summary> 所属模块名 </summary>
        public string module { get; set; }
        /// <summary> 行为唯一标识 </summary>
        public string method { get; set; }
        /// <summary> 行为描述 </summary>
        public string remark { get; set; }
        /// <summary> 需要输入的参数 </summary>
        public string param { get; set; }
        /// <summary> 日志规则 </summary>
        public string log { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
    }
}
