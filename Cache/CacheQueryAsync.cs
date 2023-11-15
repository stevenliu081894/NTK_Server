using StackExchange.Redis;
using NTKServer.Internal;
using NTKServer.Tool;
using System.Collections;
using System.Linq;
using System.Text.Json;

namespace NTKServer.Cache
{
    public class CacheQueryAsync
    {
        public static IDatabase _db = RedisEntity.SelectDb(5); // 越南

        #region public method

        public static void SelectDB(int id)
        {
            _db = RedisEntity.SelectDb(id);
        }

        public static IBatch CreateBatch()
        {
            return _db.CreateBatch();
        }

        public static async Task<RedisResult> RunScriptAsync(LuaScript script, object? param)
        {
            return await _db.ScriptEvaluateAsync(script, param);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        public static IEnumerable<string> ConvertStrings<T>(IEnumerable<T> list) where T : struct
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            return list.Select(x => x.ToString());
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
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
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default;

            using (var memoryStream = new MemoryStream(data))
            {
                var result = (T)JsonSerializer.Deserialize(memoryStream, typeof(T));
                return result;
            }
        }

        #endregion public static method

        #region String 操作

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            return await _db.StringSetAsync(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 批次保存多个字符串值
        /// </summary>
        /// <param name="redisKeyValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task StringBatchSetAsync(Dictionary<string, Queue> redisKeyValue, TimeSpan? expiry = null)
        {
            var batch = _db.CreateBatch();
            List<Task> taskList = new();
            foreach (var kv in redisKeyValue)
            {
                taskList.Add(batch.StringSetAsync(kv.Key, PublicTool.ToJson(kv.Value)));
            }
            batch.Execute();
            await Task.WhenAll(taskList);
        }

        /// <summary>
        /// 保存一组字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value));
            return await _db.StringSetAsync(pairs.ToArray());
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<string> StringGetAsync(string redisKey, TimeSpan? expiry = null)
        {
            return await _db.StringGetAsync(redisKey);
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> StringSetAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            var json = Serialize(redisValue);
            return await _db.StringSetAsync(redisKey, json, expiry);
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<T> StringGetAsync<T>(string redisKey, TimeSpan? expiry = null)
        {
            return Deserialize<T>(await _db.StringGetAsync(redisKey));
        }

        #endregion

        #region Hash

        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        public static async Task<bool> HashExistsAsync(string redisKey, string hashField)
        {
            return await _db.HashExistsAsync(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        public static async Task<bool> HashDeleteAsync(string redisKey, string hashField)
        {
            return await _db.HashDeleteAsync(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        public static async Task<long> HashDeleteAsync(string redisKey, IEnumerable<RedisValue> hashField)
        {

            return await _db.HashDeleteAsync(redisKey, hashField.ToArray());
        }

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        public static async Task<bool> HashSetAsync(string redisKey, string hashField, string value)
        {

            return await _db.HashSetAsync(redisKey, hashField, value);
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        public static async Task HashSetAsync(string redisKey, IEnumerable<HashEntry> hashFields)
        {

            await _db.HashSetAsync(redisKey, hashFields.ToArray());
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        public static async Task<RedisValue> HashGetAsync(string redisKey, string hashField)
        {

            return await _db.HashGetAsync(redisKey, hashField);
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        public static async Task<IEnumerable<RedisValue>> HashGetAsync(string redisKey, RedisValue[] hashField, string value)
        {

            return await _db.HashGetAsync(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        public static async Task<IEnumerable<RedisValue>> HashKeysAsync(string redisKey)
        {

            return await _db.HashKeysAsync(redisKey);
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        public static async Task<IEnumerable<RedisValue>> HashValuesAsync(string redisKey)
        {

            return await _db.HashValuesAsync(redisKey);
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        public static async Task<bool> HashSetAsync<T>(string redisKey, string hashField, T value)
        {

            var json = PublicTool.ToJson(value);
            return await _db.HashSetAsync(redisKey, hashField, json);
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        public static async Task<T> HashGetAsync<T>(string redisKey, string hashField)
        {

            return PublicTool.FromJson<T>(await _db.HashGetAsync(redisKey, hashField));
        }

        #endregion

        #region List 操作

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<string> ListLeftPopAsync(string redisKey)
        {
            return await _db.ListLeftPopAsync(redisKey);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<string> ListRightPopAsync(string redisKey)
        {
            return await _db.ListRightPopAsync(redisKey);
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListRemoveAsync(string redisKey, string redisValue)
        {
            return await _db.ListRemoveAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        {
            return await _db.ListRightPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        {
            return await _db.ListLeftPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<long> ListLengthAsync(string redisKey)
        {
            return await _db.ListLengthAsync(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> ListRangeAsync(string redisKey, long start = 0L, long stop = -1L)
        {
            var query = await _db.ListRangeAsync(redisKey, start, stop);
            return query.Select(x => x.ToString());
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<T> ListLeftPopAsync<T>(string redisKey)
        {
            return Deserialize<T>(await _db.ListLeftPopAsync(redisKey));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<T> ListRightPopAsync<T>(string redisKey)
        {
            return Deserialize<T>(await _db.ListRightPopAsync(redisKey));
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListRightPushAsync<T>(string redisKey, T redisValue)
        {
            return await _db.ListRightPushAsync(redisKey, Serialize(redisValue));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListLeftPushAsync<T>(string redisKey, T redisValue)
        {
            return await _db.ListLeftPushAsync(redisKey, Serialize(redisValue));
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
        public static async Task<bool> SortedSetAddAsync(string redisKey, string member, double score)
        {
            return await _db.SortedSetAddAsync(redisKey, member, score);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> SortedSetRangeByRankAsync(string redisKey)
        {
            return ConvertStrings(await _db.SortedSetRangeByRankAsync(redisKey));
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<long> SortedSetLengthAsync(string redisKey)
        {
            return await _db.SortedSetLengthAsync(redisKey);
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public static async Task<bool> SortedSetRemoveAsync(string redisKey, string memebr)
        {
            return await _db.SortedSetRemoveAsync(redisKey, memebr);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static async Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score)
        {
            var json = Serialize(member);
            return await _db.SortedSetAddAsync(redisKey, json, score);
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Task<double> SortedSetIncrementAsync(string redisKey, string member, double value = 1)
        {
            return _db.SortedSetIncrementAsync(redisKey, member, value);
        }

        #endregion

        #region key 操作

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<bool> KeyDeleteAsync(string redisKey)
        {
            return await _db.KeyDeleteAsync(redisKey);
        }

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public static async Task<long> KeyDeleteAsync(IEnumerable<string> redisKeys)
        {
            var keys = redisKeys.Select(x => (RedisKey)x);
            return await _db.KeyDeleteAsync(keys.ToArray());
        }

        /// <summary>
        /// 校验 Key 是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<bool> KeyExistsAsync(string redisKey)
        {
            return await _db.KeyExistsAsync(redisKey);
        }

        /// <summary>
        /// 重命名 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisNewKey"></param>
        /// <returns></returns>
        public static async Task<bool> KeyRenameAsync(string redisKey, string redisNewKey)
        {
            return await _db.KeyRenameAsync(redisKey, redisNewKey);
        }

        /// <summary>
        /// 设置 Key 的时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static async Task<bool> KeyExpireAsync(string redisKey, TimeSpan? expiry)
        {
            return await _db.KeyExpireAsync(redisKey, expiry);
        }

        #endregion

    }
}
