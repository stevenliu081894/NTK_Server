using NTKServer.Internal;

namespace NTKServer.ViewModels.Banner
{
    public class BannerList
    {
        /// <summary> cms_files_fk: # </summary>
        public int cms_files_fk { get; set; }

        /// <summary> 尺寸:0_PC 1_手机 </summary>
        public int size { get; set; }
        /// <summary> 尺寸:0_PC 1_手机 </summary>
        public string size_str { get; set; }

        /// <summary> enable: 启用 </summary>
        public string enable { get; set; }

        /// <summary> lang: 語言 </summary>
        public string lang { get; set; }

        /// <summary> sort: 排序 </summary>
        public int sort { get; set; }

        /// <summary> url: 網址 </summary>
        public string url { get; set; }

    }
}
