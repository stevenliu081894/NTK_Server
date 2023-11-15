using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MemberLogDto
    {
        /// <summary> 用户FK </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 操作时间 </summary>
        public DateTime addtime { get; set; }
        /// <summary> 操作ip </summary>
        public string ip { get; set; }
        /// <summary> 浏览url </summary>
        public string urlpath { get; set; }
        /// <summary> 参数 </summary>
        public string info { get; set; }
        /// <summary> url类型 </summary>
        public int urltype { get; set; }
        /// <summary> 浏览设备 </summary>
        public string udevice { get; set; }
        /// <summary>  </summary>
        public string ipinfo { get; set; }
    }
}
