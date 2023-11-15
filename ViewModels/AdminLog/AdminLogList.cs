namespace NTKServer.ViewModels.AdminLog
{
    public class AdminLogList
    {
        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> create_time: 时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> account: 帐号 </summary>
        public string account { get; set; }

        /// <summary> nickname: 管理员暱称 </summary>
        public string nickname { get; set; }

        /// <summary> remark: 日志 </summary>
        public string remark { get; set; }

        /// <summary> action_ip: ip </summary>
        public string action_ip { get; set; }

    }
}
