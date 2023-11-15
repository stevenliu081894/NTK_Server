using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MemberTaskDto
    {
        /// <summary> # </summary>
        public int pk { get; set; }
        /// <summary> 任务编号 </summary>
        public int sub_type { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
        /// <summary> 语系 </summary>
        public string lang { get; set; }
        /// <summary> 奖励金额 </summary>
        public decimal coin { get; set; }
        /// <summary> 奖励标题 </summary>
        public string title { get; set; }
        /// <summary> 奖励说明 </summary>
        public string content { get; set; }
    }
}
