using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.Dto
{
    public class CmsBannerDto
    {
        /// <summary> 图片编号  </summary>
        public int cms_files_fk { get; set; }
        /// <summary> 启用 </summary>
        public bool enable { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 图片连结网址 </summary>
        public string url { get; set; }
        /// <summary> 尺寸:0_PC 1_手机 </summary>
        public int size { get; set; }
        /// <summary> 尺寸:0_PC 1_手机 </summary>
        public List<SelectListItem> sizes { get; set; }
        /// <summary> 适用语系 </summary>
        public string lang { get; set; }

        /// <summary> cms_file.url </summary>
        public string image_url { get; set; }
        /// <summary> 照片檔案 </summary>
        public IFormFile image { get; set; }
    }
}
