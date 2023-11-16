using DB.Services;
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

    }
}
