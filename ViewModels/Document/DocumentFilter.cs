using NTKServer.Internal;

namespace NTKServer.ViewModels.Document
{
    public class DocumentFilter
    {
        /// <summary> pk: 編號 </summary>
        [Where("=", "cms_document.pk")]
        public int? pk { get; set; }

        /// <summary> cid: 文章key </summary>
        [Where("LIKE", "cms_document.cid")]
        public string? cid { get; set; }

        /// <summary> lang: 语系 </summary>
        [Where("=", "cms_document.lang")]
        public string? lang { get; set; }

        /// <summary> title: 标题 </summary>
        [Where("LIKE", "cms_document.title")]
        public string? title { get; set; }

    }
}
