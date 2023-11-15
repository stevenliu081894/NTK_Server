using NTKServer.Cache;
using NTKServer.Models;

namespace NTKServer.Libs
{
	public class TokenCatchLib
	{
		public static TokenModel? GetToken(string key)
		{
			CacheQuery.SelectDB(CacheEnum.token);
			return CacheQuery.StringGet<TokenModel>(key);
		}

		public static void SetToken(string token, TokenModel data)
		{

			CacheQuery.SelectDB(CacheEnum.token);

#if _DEBUG
            CacheQuery.StringSet(token, data, TimeSpan.FromHours(12));
#else
			CacheQuery.StringSet(token, data, TimeSpan.FromMinutes(30));
#endif
		}

		public static bool IsTokenExist(string token)
		{
			CacheQuery.SelectDB(CacheEnum.token);
#if _DEBUG
            return CacheQuery.KeyExpire(token, TimeSpan.FromHours(12));
#else
			return CacheQuery.KeyExpire(token, TimeSpan.FromMinutes(30));
#endif

		}

		public static void RemoveToken(string token)
		{
			CacheQuery.SelectDB(CacheEnum.token);
			CacheQuery.KeyDelete(token);
		}
	}
}
