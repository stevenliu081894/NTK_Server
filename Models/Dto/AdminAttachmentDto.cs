using System;
using System.Collections.Generic;

namespace Models.Dto
{
    public class AdminAttachmentDto
    {
        /// <summary> 用户id </summary>
        public int member_fk { get; set; }
        /// <summary>  </summary>
        public int pk { get; set; }
        /// <summary> 文件名 </summary>
        public string name { get; set; }
        /// <summary> 模块名，由哪个模块上传的 </summary>
        public string module { get; set; }
        /// <summary> 文件路径 </summary>
        public string path { get; set; }
        /// <summary> 缩略图路径 </summary>
        public string thumb { get; set; }
        /// <summary> 文件链接 </summary>
        public string url { get; set; }
        /// <summary> 文件mime类型 </summary>
        public string mime { get; set; }
        /// <summary> 文件类型 </summary>
        public string ext { get; set; }
        /// <summary> 文件大小 </summary>
        public int size { get; set; }
        /// <summary> 文件md5 </summary>
        public string md5 { get; set; }
        /// <summary> sha1 散列值 </summary>
        public string sha1 { get; set; }
        /// <summary> 上传驱动 </summary>
        public string driver { get; set; }
        /// <summary> 下载次数 </summary>
        public int download { get; set; }
        /// <summary> 排序 </summary>
        public int sort { get; set; }
        /// <summary> 状态 </summary>
        public int status { get; set; }
    }
}
