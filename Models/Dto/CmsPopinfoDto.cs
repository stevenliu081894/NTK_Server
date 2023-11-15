using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsPopinfoDto
    {
        /// <summary> 编号 </summary>
        public int pk { get; set; }
        /// <summary> 语系 </summary>
        public string lang { get; set; }
        /// <summary> 讯息内容 </summary>
        public string info { get; set; }
        /// <summary> 尺寸 1.手机端 2.pc </summary>
        public int size { get; set; }
        /// <summary> 启用 </summary>
        public bool enable { get; set; }
    }
}
