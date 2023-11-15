using NTKServer.Internal;

namespace NTKServer.ViewModels.Document
{
    public class DocumentList
    {
        /// <summary> pk: 編號 </summary>
        public int pk { get; set; }

        /// <summary> cid: 文章key </summary>
        public string cid { get; set; }

        /// <summary> lang: 语系 </summary>
        public string lang { get; set; }

        /// <summary> title: 标题 </summary>
        public string title { get; set; }

        /// <summary> view: 阅读量 </summary>
        public int view { get; set; }

        /// <summary> status: 状态 </summary>
        public bool status { get; set; }

    }
}
