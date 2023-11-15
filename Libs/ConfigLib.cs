using DB.Services;
using NTKServer.Cache;
using NTKServer.Internal;
using NTKServer.ViewModels.MultiLang;

namespace NTKServer.Libs
{
    /// <summary>
    /// 读取系统内建的参数值
    /// </summary>
    /// <TODO>
    /// 请改成从 DB admin_config 读取
    /// </TODO>
    public class ConfigLib
    {
        private static string ConfigCacheTable = "AdminConfig";

        public static void Reset()
        {
            CacheQuery.SelectDB(CacheEnum.admin);
            CacheQuery.KeyDelete(ConfigCacheTable);
            var configs = AdminConfigService.FindAll();
            if (configs.Count <= 0) return;
            foreach (var config in configs)
            {
                CacheQuery.HashSet(ConfigCacheTable, config.name, config.value);
            }
        }

        public static string Get(string key)
        {
            try {
                CacheQuery.SelectDB(CacheEnum.admin);

                if (!CacheQuery.KeyExists(ConfigCacheTable))
                {
                    var configs = AdminConfigService.FindAll();
                    if (configs.Count <= 0) throw new AppException("尚未配置系统设置");
                    foreach (var config in configs)
                    {
                        CacheQuery.HashSet(ConfigCacheTable, config.name, config.value);
                    }
                }

                if (!CacheQuery.HashExists(ConfigCacheTable, key))
                {
                    var config = AdminConfigService.Find(key);
                    if (config == null) throw new AppException("尚未配置系统设置");
                    CacheQuery.HashSet(ConfigCacheTable, config.name, config.value);
                }
            }
            catch (Exception ex)
            {
                LogLib.Log("[ConfigLib][Get]" + ex.Message);
                throw new AppException("取得系統設置錯誤");
            }

            return CacheQuery.HashGet(ConfigCacheTable, key);
        }
    }
}
