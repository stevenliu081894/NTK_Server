using NTKServer.Internal;

namespace NTKServer.ViewModels.MessageTemplate
{
    public class MessageTemplateFilter
    {
        /// <summary> temp_id: 模板编号 </summary>
        [Where("=", "message_template.temp_id")]
        public int? temp_id { get; set; }

        /// <summary> lang: 語系 </summary>
        [Where("=", "message_template.lang")]
        public string? lang { get; set; }

    }
}
