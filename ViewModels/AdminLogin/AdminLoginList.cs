namespace NTKServer.ViewModels.AdminLogin
{
    public class AdminLoginList
    {
        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> login_account: 帐号 </summary>
        public string login_account { get; set; }

        /// <summary> create_time: 登入时间 </summary>
        public DateTime create_time { get; set; }

        /// <summary> status: 登入结果 </summary>
        public bool status { get; set; }

        /// <summary> ip: IP </summary>
        public string ip { get; set; }

        /// <summary> ip_country: IP国家 </summary>
        public string ip_country { get; set; }

        /// <summary> remark: 说明 </summary>
        public string remark { get; set; }

        /// <summary> device: 设备 </summary>
        public string device { get; set; }

    }
}
