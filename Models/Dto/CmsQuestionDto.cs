using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsQuestionDto
    {
        /// <summary> 问题类别 </summary>
        public int question_category_fk { get; set; }

		/// <summary> 语系 </summary>
		public string lang { get; set; }

		/// <summary> 编号  </summary>
		public int pk { get; set; }
        /// <summary> 题目 </summary>
        public string question { get; set; }
        /// <summary> 回答说明 </summary>
        public string answer { get; set; }
        /// <summary> 是否启用 </summary>
        public bool enable { get; set; }
        /// <summary> 常见问题(客服页面) </summary>
        public bool commonly_used { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
    }
}
