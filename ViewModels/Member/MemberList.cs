using NTKServer.Internal;

namespace NTKServer.ViewModels.Member
{
    public class MemberList
    {
        /// <summary> admin_user_fk: 服务人員 </summary>
        public int admin_user_fk { get; set; }

		/// <summary> admin_user_fk: 服务人員 </summary>
		public string admin_user_name { get; set; }

		/// <summary> pk: 會員編號 </summary>
		public int pk { get; set; }

        /// <summary> account: 会员帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> real_name: 会员姓名 </summary>
        public string real_name { get; set; }

        /// <summary> lang: 语系 </summary>
        public string lang { get; set; }

        /// <summary> recommend_id: 推荐人 </summary>
        public string recommend_id { get; set; }

        /// <summary> invitation_code: 專屬邀请码 </summary>
        public string invitation_code { get; set; }

        /// <summary> last_login_time: 最后登录时间 </summary>
        public DateTime last_login_time { get; set; }

        /// <summary> last_login_ip: 最后登录ip </summary>
        public string last_login_ip { get; set; }

        /// <summary> level_id: 会员等级 </summary>
        public string level_id { get; set; }

        /// <summary> remark: 备注内容 </summary>
        public string remark { get; set; }

    }
}
