using NTKServer.Internal;

namespace NTKServer.ViewModels.MemberTask
{
    public class MemberTaskFilter
    {
        /// <summary> sub_type: 任务编号 </summary>
        [Where("=", "member_task.sub_type")]
        public int? sub_type { get; set; }

        /// <summary> lang: 语系 </summary>
        [Where("=", "member_task.lang")]
        public string? lang { get; set; }

    }
}
