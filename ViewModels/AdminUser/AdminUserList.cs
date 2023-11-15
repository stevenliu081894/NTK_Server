using NTKServer.Internal;

namespace NTKServer.ViewModels.AdminUser
{
    public class AdminUserList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> account: 用户名 </summary>
        public string account { get; set; }

        /// <summary> nickname: 昵称 </summary>
        public string nickname { get; set; }

        /// <summary> name: 角色名称 </summary>
        public string name { get; set; }

        /// <summary> status: 启用 </summary>
        public bool status { get; set; }

        /// <summary> is_admin: 组长 </summary>
        public bool is_admin { get; set; }

        /// <summary> sort: 排序 </summary>
        public int sort { get; set; }

        /// <summary> lang: 语系 </summary>
        public string lang { get; set; }

        /// <summary> last_login_time: 最后登录时间 </summary>
        public DateTime? last_login_time { get; set; }

    }
}
