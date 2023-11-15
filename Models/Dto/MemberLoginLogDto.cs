using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MemberLoginLogDto
    {
        /// <summary> 用户帐号pk </summary>
        public int member_fk { get; set; }
        /// <summary> 登入纪录 pk </summary>
        public int pk { get; set; }
        /// <summary>  </summary>
        public string ip { get; set; }
        /// <summary> IP登入国家 </summary>
        public string ip_country { get; set; }
        /// <summary> 登入帐号 </summary>
        public string login_account { get; set; }
        /// <summary> 设备 </summary>
        public string device { get; set; }
        /// <summary> 登入时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 登入结果：1:成功 0:失敗 </summary>
        public int status { get; set; }
        /// <summary> 备注 </summary>
        public string remark { get; set; }
    }
}
