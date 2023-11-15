using NTKServer.Internal;

namespace NTKServer.ViewModels.MessageRecord
{
    public class MessageRecordList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> nickname: 名称 </summary>
        public string nickname { get; set; }

        /// <summary> title: 标题 </summary>
        public string title { get; set; }

        /// <summary> account: 登入帐号 </summary>
        public string account { get; set; }

        /// <summary> mobile_country: 国码 </summary>
        public string mobile_country { get; set; }

        /// <summary> mobile: 手机号 </summary>
        public string mobile { get; set; }

        /// <summary> sent_time: 发信时间 </summary>
        public DateTime sent_time { get; set; }

        /// <summary> read_time: 已读时间 </summary>
        public DateTime read_time { get; set; }

    }
}
