using NTKServer.Internal;

namespace NTKServer.ViewModels.AdminLog
{
    public class AdminLogFilter
    {
        /// <summary> start_time: 起始时间 </summary>
        [Where(">=", "admin_log.create_time")]
        public DateTime? start_time { get; set; }

        /// <summary> end_time: 结束时间 </summary>
        [Where("<", "admin_log.create_time")]
        public DateTime? end_time { get; set; }

        /// <summary> pk: 编号 </summary>
        [Where("=", "admin_log.pk")]
        public int? pk { get; set; }

        /// <summary> account: 帐号 </summary>
        [Where("=", "admin_user.account")]
        public string? account { get; set; }

        /// <summary> remark: 日志 </summary>
        [Where("LIKE", "admin_log.remark")]
        public string? remark { get; set; }

        /// <summary> action_ip: ip </summary>
        [Where("=", "admin_log.action_ip")]
        public string? action_ip { get; set; }

    }
}
