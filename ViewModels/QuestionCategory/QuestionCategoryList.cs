using NTKServer.Internal;

namespace NTKServer.ViewModels.QuestionCategory
{
    public class QuestionCategoryList
    {
        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> enable: 启用 </summary>
        public bool enable { get; set; }

        /// <summary> label: 名称 </summary>
        public string label { get; set; }

        /// <summary> icon: 图示网址 </summary>
        public string icon { get; set; }

        /// <summary> sort: 排序 </summary>
        public int sort { get; set; }

        /// <summary> url: 網址 </summary>
        public string image_url { get; set; }
        /// <summary> 語系 </summary>
        public string lang { get; set; }

    }
}
