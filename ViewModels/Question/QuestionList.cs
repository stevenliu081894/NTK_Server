using NTKServer.Internal;

namespace NTKServer.ViewModels.Question
{
    public class QuestionList
    {
        /// <summary> question_category_fk: 问题类别 </summary>
        public int question_category_fk { get; set; }

		/// <summary> 问题类别 </summary>
		public string question_category_text { get; set; }

        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> lang: 语系 </summary>
        public string lang { get; set; }

        /// <summary> question: 题目 </summary>
        public string question { get; set; }

        /// <summary> enable: 启用 </summary>
        public bool enable { get; set; }

        /// <summary> commonly_used: 客服页面 </summary>
        public bool commonly_used { get; set; }

        /// <summary> sort: 排序 </summary>
        public int sort { get; set; }

    }
}
