using StackExchange.Redis;
using NTKServer.Internal;
using NTKServer.Tool;
using System.Text.Json;

namespace NTKServer.Cache
{
    public class CacheQuery
    {
        private static IDatabase _db = RedisEntity.SelectDb(0);

        #region public method

        public static void SelectDB(CacheEnum id)
        {
            _db = RedisEntity.SelectDb((int)id);
        }

        public static IBatch CreateBatch()
        {
            return _db.CreateBatch();
        }

        public static RedisResult RunScript(LuaScript script, object? param)
        {
            return _db.ScriptEvaluate(script, param);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        private static IEnumerable<string?> ConvertStrings<T>(IEnumerable<T> list) where T : struct
        {
            if (list == null) return null;
            return list.Select(x => x.ToString());
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                JsonSerializer.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        private static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default;

            using (var memoryStream = new MemoryStream(data))
            {
                var result = (T?)JsonSerializer.Deserialize(memoryStream, typeof(T));
                return result;
            }
        }

        #endregion private static method

        #region String 操作

        /// <summary>
        /// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        public static bool StringSet(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            return _db.StringSet(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 保存多个 Key-value
        /// </summary>
        public static bool StringSet(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        {
            keyValuePairs =
                keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value));
            return _db.StringSet(keyValuePairs.ToArray());
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        public static string StringGet(string redisKey)
        {
            return _db.StringGet(redisKey);
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        public static bool StringSet<T>(string redisKey, T? redisValue, TimeSpan? expiry = null)
        {
            var json = (redisValue == null ? string.Empty : PublicTool.ToJson(redisValue));
            return _db.StringSet(redisKey, json, expiry);
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        public static T StringGet<T>(string redisKey)
        {
            return PublicTool.FromJson<T>(_db.StringGet(redisKey));
        }

        #endregion 

        #region hash
        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        public static bool HashExists(string redisKey, string hashField)
        {
            return _db.HashExists(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>

        public static bool HashDelete(string redisKey, string hashField)
        {
            return _db.HashDelete(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        public static long HashDelete(string redisKey, IEnumerable<RedisValue> hashField)
        {

            return _db.HashDelete(redisKey, hashField.ToArray());
        }

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        public static bool HashSet(string redisKey, string hashField, string value)
        {
            return _db.HashSet(redisKey, hashField, value);
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        public static void HashSet(string redisKey, IEnumerable<HashEntry> hashFields)
        {

            _db.HashSet(redisKey, hashFields.ToArray());
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        public static RedisValue HashGet(string redisKey, string hashField)
        {

            return _db.HashGet(redisKey, hashField);
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        public static RedisValue[] HashGet(string redisKey, RedisValue[] hashField, string value)
        {

            return _db.HashGet(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        public static IEnumerable<RedisValue> HashKeys(string redisKey)
        {

            return _db.HashKeys(redisKey);
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        public static RedisValue[] HashValues(string redisKey)
        {
            return _db.HashValues(redisKey);
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        public static bool HashSet<T>(string redisKey, string hashField, T value)
        {
            var json = PublicTool.ToJson(value);
            return _db.HashSet(redisKey, hashField, json);
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        public static T HashGet<T>(string redisKey, string hashField)
        {
            return PublicTool.FromJson<T>(_db.HashGet(redisKey, hashField));
        }
        #endregion

        #region List 操作

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static string ListLeftPop(string redisKey)
        {
            return _db.ListLeftPop(redisKey);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static string ListRightPop(string redisKey)
        {
            return _db.ListRightPop(redisKey);
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListRemove(string redisKey, string redisValue)
        {
            return _db.ListRemove(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListRightPush(string redisKey, string redisValue)
        {
            return _db.ListRightPush(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListLeftPush(string redisKey, string redisValue)
        {
            return _db.ListLeftPush(redisKey, redisValue);
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static long ListLength(string redisKey)
        {
            return _db.ListLength(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        public static IEnumerable<string> ListRange(string redisKey, long start = 0L, long stop = -1L)
        {
            return ConvertStrings(_db.ListRange(redisKey, start, stop));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static T ListLeftPop<T>(string redisKey)
        {
            return Deserialize<T>(_db.ListLeftPop(redisKey));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static T ListRightPop<T>(string redisKey)
        {
            return Deserialize<T>(_db.ListRightPop(redisKey));
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListRightPush<T>(string redisKey, T redisValue)
        {
            return _db.ListRightPush(redisKey, Serialize(redisValue));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListLeftPush<T>(string redisKey, T redisValue)
        {
            return _db.ListLeftPush(redisKey, Serialize(redisValue));
        }

        #endregion

        #region SortedSet 操作

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool SortedSetAdd(string redisKey, string member, double score)
        {
            return _db.SortedSetAdd(redisKey, member, score);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IEnumerable<string> SortedSetRangeByRank(string redisKey, long start = 0L, long stop = -1L,
            SortEnum order = SortEnum.Ascending)
        {
            return _db.SortedSetRangeByRank(redisKey, start, stop, (Order)order).Select(x => x.ToString());
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static long SortedSetLength(string redisKey)
        {
            return _db.SortedSetLength(redisKey);
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public static bool SortedSetLength(string redisKey, string memebr)
        {
            return _db.SortedSetRemove(redisKey, memebr);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool SortedSetAdd<T>(string redisKey, T member, double score)
        {
            var json = Serialize(member);

            return _db.SortedSetAdd(redisKey, json, score);
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double SortedSetIncrement(string redisKey, string member, double value = 1)
        {
            return _db.SortedSetIncrement(redisKey, member, value);
        }

        #endregion

        #region key 操作

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static bool KeyDelete(string redisKey)
        {
            return _db.KeyDelete(redisKey);
        }

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public static long KeyDelete(IEnumerable<string> redisKeys)
        {
            var keys = redisKeys.Select(x => (RedisKey)x);
            return _db.KeyDelete(keys.ToArray());
        }

        /// <summary>
        /// 校验 Key 是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static bool KeyExists(string redisKey)
        {
            return _db.KeyExists(redisKey);
        }

        /// <summary>
        /// 重命名 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisNewKey"></param>
        /// <returns></returns>
        public static bool KeyRename(string redisKey, string redisNewKey)
        {
            return _db.KeyRename(redisKey, redisNewKey);
        }

        /// <summary>
        /// 设置 Key 的时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool KeyExpire(string redisKey, TimeSpan? expiry)
        {
            return _db.KeyExpire(redisKey, expiry);
        }

        #endregion
    }
}
