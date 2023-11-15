namespace NTKServer.ViewModels.CmsPopinfo
{
    public class CmsPopinfoList
    {
        /// <summary> pk: 编号 </summary>
        public int pk { get; set; }

        /// <summary> lang: 语系 </summary>
        public string lang { get; set; }

        /// <summary> info: 讯息内容 </summary>
        public string info { get; set; }

        /// <summary> size: 尺寸 </summary>
        public int size { get; set; }

        /// <summary> enable: 启用 </summary>
        public bool enable { get; set; }

    }
}
