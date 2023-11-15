using NTKServer.Internal;

namespace NTKServer.ViewModels.Bulletin
{
    public class BulletinFilter
    {
        /// <summary> pk: # </summary>
        [Where("=", "pk")]
        public int? pk { get; set; }

        /// <summary> lang: 語系 </summary>
        [Where("=", "lang")]
        public string? lang { get; set; }

        /// <summary> title: 标题 </summary>
        [Where("LIKE", "title")]
        public string? title { get; set; }

    }
}
