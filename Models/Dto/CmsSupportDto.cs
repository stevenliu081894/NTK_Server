using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsSupportDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 語系 </summary>
        public string lang { get; set; }
        /// <summary> 聯繫電話 </summary>
        public string svc_phone { get; set; }
        /// <summary> 工作時間 </summary>
        public string svc_workday { get; set; }
        /// <summary> 非工作日 </summary>
        public string svc_nonworkday { get; set; }
        /// <summary> 客服信箱 </summary>
        public string svc_email { get; set; }
        /// <summary> 线上客服链接 </summary>
        public string svc_link { get; set; }
    }
}
