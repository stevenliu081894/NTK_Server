using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class CmsFilesDto
    {
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 網址 </summary>
        public string url { get; set; }
        /// <summary> 文件類型 1_圖片 2_文件 </summary>
        public int file_type { get; set; }
        /// <summary> 表 </summary>
        public string table { get; set; }
        /// <summary> 對應的鍵值 </summary>
        public int key { get; set; }
    }
}
