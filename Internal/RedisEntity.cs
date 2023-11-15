using StackExchange.Redis;
using NTKServer.Libs;

namespace NTKServer.Internal
{
    public class RedisEntity
    {
        private static IConfiguration _config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json").Build();

        public static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            try
            {
                string redisCacheConnection = _config["RedisCacheUrl"];
                return ConnectionMultiplexer.Connect(redisCacheConnection);
            }
            catch (RedisConnectionException ex)
            {
                LogLib.Log(ex.Message);
                throw new AppException(1020, "redis_exception");
            }
        });

        public static IDatabase SelectDb(int dbNum = 0)
        {
            var _redis = lazyConnection.Value;
            return _redis.GetDatabase(dbNum);
        }
    }
}
