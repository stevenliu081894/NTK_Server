namespace NTKServer.ViewModels.AdminConfig
{
    public class AdminConfigList
    {
        /// <summary> name: 键值 </summary>
        public string name { get; set; }

        /// <summary> title: 标题 </summary>
        public string title { get; set; }

        /// <summary> group: 分类 </summary>
        public string group { get; set; }

        /// <summary> value: 配置值 </summary>
        public string value { get; set; }

        /// <summary> status: 状态：0禁用，1启用 </summary>
        public bool status { get; set; }

    }
}
