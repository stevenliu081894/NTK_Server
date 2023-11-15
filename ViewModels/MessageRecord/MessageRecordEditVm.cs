
namespace NTKServer.ViewModels.MessageRecord
{
    public class MessageRecordEditVm
    {
		/// <summary> receiver_fk: 接收者_pk </summary>
		public int receiver_fk { get; set; }

		/// <summary> account: 会员帐号 </summary>
		public string account { get; set; }

		/// <summary> nickname: 会员姓名 </summary>
		public string nickname { get; set; }

		/// <summary> message_record: 标题 </summary>
		public string title { get; set; }

        /// <summary> message_record: 消息内容 </summary>
        public string info { get; set; }

        /// <summary> read_time: 已讀時間 </summary>
        public DateTime? read_time { get; set; }

    }
}
