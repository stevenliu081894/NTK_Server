using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class VerifyDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary>  </summary>
        public string code { get; set; }
        /// <summary>  </summary>
        public DateTime send_time { get; set; }
        /// <summary> 1:邮件激活验证 </summary>
        public int type { get; set; }
        /// <summary>  </summary>
        public string email { get; set; }
    }
}
