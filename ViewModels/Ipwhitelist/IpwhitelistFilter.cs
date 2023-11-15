using NTKServer.Internal;

namespace NTKServer.ViewModels.Ipwhitelist
{
    public class IpwhitelistFilter
    {
        /// <summary> ip: ip地址 </summary>
        [Where("=", "ip")]
        public string? ip { get; set; }

        /// <summary> remarks: 备注 </summary>
        [Where("LIKE", "remarks")]
        public string? remarks { get; set; }

    }
}
