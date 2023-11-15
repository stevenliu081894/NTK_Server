namespace NTKServer.Tool
{
    public class TimeTool
    {
        /// <summary>
        /// 將地區時間轉換為 UTC 時間
        /// </summary>
        public static DateTime ConvertLocalToUtc(DateTime localTime)
            => TimeZoneInfo.ConvertTimeToUtc(localTime, TimeZoneInfo.Local);
    }
}
