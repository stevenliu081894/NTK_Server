using NTKServer.Internal;

namespace NTKServer.ViewModels.Marquee
{
    public class MarqueeList
    {
		public int pk { get; set; }
		/// <summary> lang: 語系 </summary>
		public string lang { get; set; }

        /// <summary> enable: 啟用 </summary>
        public bool enable { get; set; }

        /// <summary> msg: 輪播內容 </summary>
        public string msg { get; set; }

    }
}
