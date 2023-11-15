using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class PacketWechatAreaDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 国家名称 </summary>
        public string country { get; set; }
        /// <summary> 省份名称 </summary>
        public string province { get; set; }
        /// <summary> 城市名称 </summary>
        public string city { get; set; }
    }
}
