namespace NTKServer.ViewModels.MessageTemplate
{
    public class MessageTemplateList
    {
		public int pk {  get; set; }
		/// <summary> temp_id: 模板编号 </summary>
		public int temp_id { get; set; }

        /// <summary> lang: 語系 </summary>
        public string lang { get; set; }

        /// <summary> status: 状态 </summary>
        public bool status { get; set; }

        /// <summary> name: 说明 </summary>
        public string name { get; set; }

        /// <summary> type: 分类 </summary>
        public int type { get; set; }

        /// <summary> title: 站内信标题 </summary>
        public string title { get; set; }

        /// <summary> title_param: 站内信参数 </summary>
        public string title_param { get; set; }

        /// <summary> param: 参数 </summary>
        public string param { get; set; }

        /// <summary> template: 模板 </summary>
        public string template { get; set; }

        /// <summary> remark: 备注 </summary>
        public string remark { get; set; }

    }
}
