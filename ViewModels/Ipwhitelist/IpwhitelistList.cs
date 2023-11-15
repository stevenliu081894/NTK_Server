using NTKServer.Internal;

namespace NTKServer.ViewModels.Ipwhitelist
{
    public class IpwhitelistList
    {
        /// <summary> ip: ip地址 </summary>
        public string ip { get; set; }

        /// <summary> remarks: 备注 </summary>
        public string remarks { get; set; }

        /// <summary> status: 启用 </summary>
        public string status { get; set; }

        /// <summary> create_time: 添加时间 </summary>
        public string create_time { get; set; }

        /// <summary> update_time: 修改时间 </summary>
        public string update_time { get; set; }

    }
}
