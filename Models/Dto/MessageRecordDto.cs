using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class MessageRecordDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 0_不是 1_群发信 </summary>
        public bool isbatch { get; set; }
        /// <summary> 1_member 2_admin_user 3_agent </summary>
        public AccountTypeEnum receiver_table { get; set; }
        /// <summary> 接收者_pk </summary>
        public int receiver_fk { get; set; }
        /// <summary> 1_member 2_admin_user 3_agent </summary>
        public AccountTypeEnum sender_table { get; set; }
        /// <summary> 发送者_pk </summary>
        public int sender_fk { get; set; }
        /// <summary> 标题 </summary>
        public string title { get; set; }
        /// <summary> 消息内容 </summary>
        public string info { get; set; }
        /// <summary> 查看状态 0_未读 1_已查看  2.已删除 </summary>
        public MessageReadStatusEnum read_status { get; set; }
        /// <summary> 1_主信 2_回覆信 3_寄件備份 </summary>
        public int type { get; set; }
        /// <summary> 状态 0_草稿 1_已发送 2_发送失败 </summary>
        public MessageSendStatus send_status { get; set; }
        /// <summary> 1_站内信 2_邮箱 3_简讯 </summary>
        public MessageTransTypeEnum send_type { get; set; }
        /// <summary> 建立时间 </summary>
        public DateTime create_time { get; set; }
        /// <summary> 已读时间 </summary>
        public DateTime? read_time { get; set; }
        /// <summary> 寄信时间 </summary>
        public DateTime sent_time { get; set; }
        /// <summary> 信件發送類型 1 系統發送 2 sender_fk發送 </summary>
        public int classify { get; set; }
    }
}
