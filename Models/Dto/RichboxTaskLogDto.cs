using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RichboxTaskLogDto
    {
        /// <summary> 会员 pk </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 错误代码 </summary>
        public string code { get; set; }
        /// <summary> 错误说明 </summary>
        public string info { get; set; }
        /// <summary> 发生时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 状态 0:失败 1:成功 </summary>
        public bool result { get; set; }
    }
}
