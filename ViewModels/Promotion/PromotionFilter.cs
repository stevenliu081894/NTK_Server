using NTKServer.Internal;

namespace NTKServer.ViewModels.Promotion
{
    public class PromotionFilter
    {
        /// <summary> lang: 語系 </summary>
        [Where("=", "cms_promotion.lang")]
        public string? lang { get; set; }

        /// <summary> title: 标题 </summary>
        [Where("LIKE", "cms_promotion.title")]
        public string? title { get; set; }

        /// <summary> starttime: 开始时间 </summary>
        [Where(">=", "cms_promotion.starttime")]
        public DateTime? starttime { get; set; }

        /// <summary> endtime: 结束时间 </summary>
        [Where("<", "cms_promotion.endtime")]
        public DateTime? endtime { get; set; }

    }
}
