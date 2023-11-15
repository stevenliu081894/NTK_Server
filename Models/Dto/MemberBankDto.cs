using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MemberBankDto
    {
        /// <summary> 会员FK </summary>
        public int member_fk { get; set; }
        /// <summary> 卡片pk(英文字母+隨機數字) </summary>
        public string card_pk { get; set; }
        /// <summary> 卡片类别 1.银行卡 2.虚拟币地址 </summary>
        public int card_type { get; set; }
        /// <summary> 币别 </summary>
        public string currency { get; set; }
        /// <summary> 所属国家 </summary>
        public string country { get; set; }
        /// <summary> 所属银行 </summary>
        public string bank { get; set; }
        /// <summary> 分行 </summary>
        public string branch { get; set; }
        /// <summary> 銀行卡號 </summary>
        public string card { get; set; }
        /// <summary> 存折持有人姓名 </summary>
        public string account { get; set; }
        /// <summary> 封面照片 </summary>
        public int cms_files_fk { get; set; }
        /// <summary> 提款卡已核实 </summary>
        public bool is_confirm { get; set; }
        /// <summary> 是否已删除 </summary>
        public bool is_delete { get; set; }
        /// <summary> 建立IP </summary>
        public string create_ip { get; set; }
        /// <summary> 建立时间 </summary>
        public DateTime create_time { get; set; }
    }
}
