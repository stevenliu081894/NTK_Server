using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsQuestionCategoryDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 类别是否启用 </summary>
        public bool enable { get; set; }
        /// <summary> 名称 </summary>
        public string label { get; set; }
        /// <summary> 图示网址 </summary>
        public string icon { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 語系 </summary>
        public string lang { get; set; }

		/// <summary> cms_file: 图片网址路径 </summary>
		public IFormFile image { get; set; }

	}
}
