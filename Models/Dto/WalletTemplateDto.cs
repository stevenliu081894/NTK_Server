using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class WalletTemplateDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 模板id </summary>
        public int temp_id { get; set; }
        /// <summary> 语系 </summary>
        public string lang { get; set; }
        /// <summary> 说明 </summary>
        public string name { get; set; }
        /// <summary> 模板 </summary>
        public string template { get; set; }
        /// <summary> 参数说明 </summary>
        public string param { get; set; }
        /// <summary> 范例 </summary>
        public string demo { get; set; }
    }
}
