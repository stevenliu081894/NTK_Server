using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MessageTemplateDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary>  </summary>
        public int temp_id { get; set; }
        /// <summary> 語系 </summary>
        public string lang { get; set; }
        /// <summary> 状态 0.停用 1.启用 </summary>
        public int status { get; set; }
        /// <summary> 说明 </summary>
        public string name { get; set; }
        /// <summary> 1.站内信 2.邮件 3.简讯 </summary>
        public int type { get; set; }
        /// <summary> 接收对象 </summary>
        public string receiver { get; set; }
        /// <summary> 站内信标题 </summary>
        public string title { get; set; }
        /// <summary> 站内信参数 </summary>
        public string title_param { get; set; }
        /// <summary> 参数 </summary>
        public string param { get; set; }
        /// <summary> 模板 </summary>
        public string template { get; set; }
        /// <summary> 备注 </summary>
        public string remark { get; set; }
    }
}
