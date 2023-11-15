using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsBulletinDto
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
        /// <summary> 上架时间 </summary>
        public DateTime starttime { get; set; }
        /// <summary> 下架时间 </summary>
        public DateTime endtime { get; set; }
        /// <summary> 内容 </summary>
        public string topic_content { get; set; }
        /// <summary> 新聞缩略图 </summary>
        public int imgid { get; set; }
        /// <summary> 新聞简介 </summary>
        public string summary { get; set; }
        /// <summary> 內容是外部鏈接模式 </summary>
        public bool outsite { get; set; }
        /// <summary> 外部鏈接網址 </summary>
        public string url { get; set; }
    }
}
