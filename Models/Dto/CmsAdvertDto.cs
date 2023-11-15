using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsAdvertDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 分类id </summary>
        public int typeid { get; set; }
        /// <summary> 广告位标识 </summary>
        public string tagname { get; set; }
        /// <summary> 广告类型 </summary>
        public int ad_type { get; set; }
        /// <summary> 时间限制:0-永不过期,1-在设内时间内有效 </summary>
        public int timeset { get; set; }
        /// <summary> 开始时间 </summary>
        public DateTime start_time { get; set; }
        /// <summary> 结束时间 </summary>
        public DateTime end_time { get; set; }
        /// <summary> 广告位名称 </summary>
        public string name { get; set; }
        /// <summary> 广告内容 </summary>
        public string content { get; set; }
        /// <summary> 过期显示内容 </summary>
        public string expcontent { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
        /// <summary>  </summary>
        public int bnr_id { get; set; }
        /// <summary>  </summary>
        public int marq_id { get; set; }
        /// <summary>  </summary>
        public string pop_msg { get; set; }
        /// <summary>  </summary>
        public bool show_pop { get; set; }
        /// <summary>  </summary>
        public string lang { get; set; }
    }
}
