using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminUserDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 用户名 </summary>
        public string account { get; set; }
        /// <summary> 用戶角色 </summary>
        public int role { get; set; }
        /// <summary> 密码 </summary>
        public string password { get; set; }
        /// <summary> 状态：0禁用，1启用 </summary>
        public int status { get; set; }
        /// <summary> 昵称 </summary>
        public string nickname { get; set; }
        /// <summary> 是否为组长 0 否，1 是 </summary>
        public int is_admin { get; set; }
        /// <summary> 手机号码 </summary>
        public string mobile { get; set; }
        /// <summary> 邮箱地址 </summary>
        public string email { get; set; }
        /// <summary> 头像 </summary>
        public int avatar { get; set; }

        /// <summary> 排序 </summary>
        public int sort { get; set; }

        /// <summary> 语系 </summary>
        public string lang { get; set; }

        /// <summary> 最后一次登录时间 </summary>
        public DateTime? last_login_time { get; set; }

        /// <summary> 登录ip </summary>
        public string? last_login_ip { get; set; }

        /// <summary> 0. 不需变更密码 1. 登入成功需变更密码 </summary>
        public bool change_password { get; set; }
    }
}
