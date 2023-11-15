using DB.Services;
using Newtonsoft.Json.Linq;
using NTKServer.Cache;
using NTKServer.Tool;

namespace NTKServer.Libs
{
    /// <summary>
    /// 多国语言翻译机
    /// </summary>
    public class LanguageLib
    {
        public static string GetWarningMsg(string lang)
        {
            var msg = MutilangCacheService.Find("mutilang_index", lang.ToLower());
            if (msg != null) {
                return msg.value;
            }
            return MutilangService.Find("warning").template;
        }

        public static void InitErrorMsgCache()
        {
            Cache.CacheQuery.SelectDB(CacheEnum.language);
            var multilang_subjects = MutilangSubjectService.FindAll();
            foreach (var mutilang in multilang_subjects) {
                InitLangErrorMsgCache(mutilang.lang.ToLower());
            }
        }

        public static void InitLangErrorMsgCache(string lang)
        {

            JObject warningMsg = JObject.Parse(GetWarningMsg(lang));
            foreach (var property in warningMsg.Properties())
            {
                string key = property.Name;
                string value = property.Value.ToString();
                string table = $"{lang}_error";
                CacheQuery.HashSet(table, key, value); ;
            }
        }
        /// <summary>
        /// 取得系統錯誤語系
        /// </summary>
        public static string GetErrorTranslate(string lang, string key)
        {
            return key;
            //// redis 已经无法连线了, 不需要再访问
            //if (key == "redis_exception")
            //{
            //    return "翻译檔不存在";
            //}

            //lang = lang.ToLower();
            //CacheQuery.SelectDB(CacheEnum.language);
            //string table = $"{lang}_error";
            //if (CacheQuery.HashExists(table, key) == false)
            //    return key + "(找不到翻译档)";

            //return CacheQuery.HashGet(table, key);
        }
    }
}
