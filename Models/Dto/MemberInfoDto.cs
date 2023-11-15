using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MemberInfoDto
    {
        /// <summary>  </summary>
        public int member_fk { get; set; }
        /// <summary> 首存时间 </summary>
        public DateTime fristtime_save_money { get; set; }
        /// <summary> 最后一次存款时间 </summary>
        public DateTime lasttime_save_money { get; set; }
    }
}
