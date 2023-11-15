namespace NTKServer.ViewModels.CmsSupport
{
    public class CmsSupportList
    {
        /// <summary> pk: # </summary>
        public int pk { get; set; }

        /// <summary> lang: 語系 </summary>
        public string lang { get; set; }

        /// <summary> svc_phone: 聯繫電話 </summary>
        public string svc_phone { get; set; }

        /// <summary> svc_workday: 工作時間 </summary>
        public string svc_workday { get; set; }

        /// <summary> svc_nonworkday: 非工作日 </summary>
        public string svc_nonworkday { get; set; }

        /// <summary> svc_email: 客服信箱 </summary>
        public string svc_email { get; set; }

        /// <summary> svc_link: 线上客服链接 </summary>
        public string svc_link { get; set; }

    }
}
