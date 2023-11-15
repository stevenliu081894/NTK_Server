using NTKServer.Internal;

namespace NTKServer.ViewModels.Promotion
{
    public class PromotionList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> lang: 語系 </summary>
        public string lang { get; set; }

        /// <summary> title: 标题 </summary>
        public string title { get; set; }

        /// <summary> view: 点击量 </summary>
        public int view { get; set; }

        /// <summary> sort: 排序 </summary>
        public int sort { get; set; }

        /// <summary> on_active: 启用 </summary>
        public bool on_active { get; set; }

        /// <summary> trash: 回收站 </summary>
        public bool trash { get; set; }

        /// <summary> starttime: 开始时间 </summary>
        public DateTime starttime { get; set; }

        /// <summary> endtime: 结束时间 </summary>
        public DateTime endtime { get; set; }

    }
}
