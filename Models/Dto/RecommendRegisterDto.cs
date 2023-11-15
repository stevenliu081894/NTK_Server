using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class RecommendRegisterDto
    {
        /// <summary> 邀请人FK </summary>
        public int member_fk { get; set; }
        /// <summary> 被邀請人 </summary>
        public int invitee_fk { get; set; }
        /// <summary> 被邀請人暱稱 </summary>
        public string nickname { get; set; }
        /// <summary> 註冊時間 </summary>
        public DateTime register_date { get; set; }
        /// <summary> 第一筆交易時間 </summary>
        public DateTime? first_borrow_date { get; set; }
    }
}
