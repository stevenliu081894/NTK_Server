using System.Security.Principal;
using MaxMind.GeoIP2;
using Newtonsoft.Json;
using NTKServer.Internal;
using NTKServer.Libs;

namespace NTKServer.Tool
{
    public class PublicTool
    {
		public static string Text(object? obj)
		{
			return (obj ?? "").ToString() ?? "";
		}
		
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch(Exception e) 
            {
                LogLib.Log(e.Message);
                return default(T);
            }
        }

		public static string GetRandomNum(int length)
		{
			Random random = new Random();
			string randomString = "";
			for (int i = 0; i < length; i++)
			{
				int randomNumber = random.Next(0, 10);
				randomString += randomNumber.ToString();
			}
			return randomString;
		}

        public static string GenerateRandomName(int length) {
            const string chars =
                "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                                .Select(s => s[new Random().Next(s.Length)])
                                .ToArray());
        }

        public static T convertUtcToLocalTime<T>(T obj)
        {
            var properties = obj.GetType().GetProperties();
            int time_zone_difference = Convert.ToInt32(ConfigLib.Get("time_zone_difference"));
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(DateTime))
                {
                    DateTime currentValue = (DateTime)property.GetValue(obj);
                    property.SetValue(obj, currentValue.AddHours(time_zone_difference));
                }
            }
            return obj;
        }

        public static T convertLocalToUtcTime<T>(T obj)
        {
            var properties = obj.GetType().GetProperties();
            int time_zone_difference = Convert.ToInt32(ConfigLib.Get("time_zone_difference"));
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(DateTime))
                {
                    DateTime currentValue = (DateTime)property.GetValue(obj);
                    property.SetValue(obj, currentValue.AddHours(-time_zone_difference));
                }
            }
            return obj;
        }

		private static List<string> excludeIp = new List<string>() { "127.0.0.1", "localhost", "::1" };

		/// <summary>
		/// 查詢國家
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static string GetCountryByIp(string ip)
		{
			try
			{
				if (excludeIp.Contains(ip)) return null;
				using var reader = new DatabaseReader("GeoLite2-Country.mmdb");
				if (string.IsNullOrEmpty(ip) == true) return ("未知");
				if (ip == "::1") return ("本地");

				var resourse = reader.Country(ip);
				if (resourse == null) return ("未知");

				return resourse.Country.Names["zh-CN"];
			}
			catch (AppException ex)
			{
				throw new AppException($"請求:{ip}，錯誤:{ex.Message}");
			}
		}

	}
}
