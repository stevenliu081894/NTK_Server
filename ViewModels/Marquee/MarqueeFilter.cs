using NTKServer.Internal;

namespace NTKServer.ViewModels.Marquee
{
    public class MarqueeFilter
    {
        /// <summary> lang: 語系 </summary>
        [Where("", "cms_marq.lang")]
        public string? lang { get; set; }

    }
}
