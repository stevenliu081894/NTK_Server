using NTKServer.Internal;

namespace NTKServer.ViewModels.CmsPopinfo
{
    public class CmsPopinfoFilter
    {
        /// <summary> lang: 语系 </summary>
        [Where("=", "lang")]
        public string? lang { get; set; }

        /// <summary> size: 尺寸 </summary>
        [Where("=", "size")]
        public int? size { get; set; }

    }
}
