using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminIpwhitelistDto
    {
        /// <summary> ip地址 </summary>
        public string ip { get; set; }
        /// <summary> 备注 </summary>
        public string remarks { get; set; }
        /// <summary> 操作人 </summary>
        public string account { get; set; }
        /// <summary> 状态,0:禁用,1:启用 </summary>
        public int status { get; set; }
        /// <summary> 添加时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 修改时间 </summary>
        public DateTime update_time { get; set; }
    }
}
