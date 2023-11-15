using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminLogDto
    {
        /// <summary> 主键 </summary>
        public int pk { get; set; }
        /// <summary> 行为 PK </summary>
        public int admin_action { get; set; }
        /// <summary> 执行用户 PK </summary>
        public int admin_user { get; set; }
        /// <summary> 触发行为的 Table Name </summary>
        public string table_name { get; set; }
        /// <summary> 触发行为的 Table PK </summary>
        public string table_index { get; set; }
        /// <summary> 执行行为者 ip </summary>
        public string action_ip { get; set; }
        /// <summary> 传入的 list </summary>
        public string param { get; set; }
        /// <summary> 日志备注 </summary>
        public string remark { get; set; }
        /// <summary> 执行行为的时间 </summary>
        public DateTime create_time { get; set; }
    }
}
