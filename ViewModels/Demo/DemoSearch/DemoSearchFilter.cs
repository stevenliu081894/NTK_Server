using NTKServer.Internal;

namespace NTKServer.ViewModels.Demo
{
    public class DemoSearchFilter
    {
        [Where("=", "pk")]
        public int? pk { get; set; }

        [Where("=", "sn")]
        public string? sn { get; set; }

        [Where("like", "sub_account")]
        public string? sub_account { get; set; }

        [Where(">=", "request_time")]
        public DateTime? beginTime { get; set; }

        [Where("<", "request_time")]
        public DateTime? endTime { get; set; }
    }
}
