using NTKServer.Internal;

namespace NTKServer.ViewModels.MessageRecord
{
    public class MessageRecordFilter
    {
        /// <summary> pk: # </summary>
        [Where("=", "message_record.pk")]
        public int? pk { get; set; }

        /// <summary> nickname: 名称 </summary>
        [Where("LIKE", "nickname")]
        public string? nickname { get; set; }

        /// <summary> title: 标题 </summary>
        [Where("LIKE", "title")]
        public string? title { get; set; }

        /// <summary> account: 登入帐号 </summary>
        [Where("=", "account")]
        public string? account { get; set; }
    }
}
