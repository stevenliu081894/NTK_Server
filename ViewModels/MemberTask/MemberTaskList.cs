namespace NTKServer.ViewModels.MemberTask
{
    public class MemberTaskList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> sub_type: 任务编号 </summary>
        public int sub_type { get; set; }

        /// <summary> currency: 币别 </summary>
        public string currency { get; set; }

        /// <summary> coin: 奖励金额 </summary>
        public decimal coin { get; set; }

        /// <summary> lang: 语系 </summary>
        public string lang { get; set; }

        /// <summary> title: 奖励标题 </summary>
        public string title { get; set; }

        /// <summary> content: 奖励说明 </summary>
        public string content { get; set; }

    }
}
