using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Dto
{
    public class CmsPromotionDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 語系 </summary>
        public string lang { get; set; }
        /// <summary> 标题 </summary>
        public string title { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 是否启用 </summary>
        public bool on_active { get; set; }
        /// <summary> 点击量 </summary>
        public int view { get; set; }
        /// <summary> 回收站 </summary>
        public bool trash { get; set; }
        /// <summary> 开始时间 </summary>
        public DateTime starttime { get; set; } = DateTime.UtcNow;
        /// <summary> 结束时间 </summary>
        public DateTime endtime { get; set; } = DateTime.UtcNow;
        /// <summary> 内容 </summary>
        public string topic_content { get; set; }
        /// <summary> 判断是要显示内页，还是跳转到网站 </summary>
        public bool outsite { get; set; }
        /// <summary> 图片网址 </summary>
        public string? img_url { get; set; }
        /// <summary> 图片檔案 </summary>
        public IFormFile? img_file { get; set; } = null;
        /// <summary> 开启网址 </summary>
        public string? url { get; set; }
        /// <summary> 活动简介 </summary>
        public string summary { get; set; }
    }
}
