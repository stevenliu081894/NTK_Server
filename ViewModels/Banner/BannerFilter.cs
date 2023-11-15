using NTKServer.Internal;

namespace NTKServer.ViewModels.Banner
{
    public class BannerFilter
    {
        /// <summary> size: 尺寸 </summary>
        [Where("=", "cms_banner.size")]
        public int? size { get; set; }

        /// <summary> lang: 語言 </summary>
        [Where("=", "cms_banner.lang")]
        public string? lang { get; set; }

    }
}
