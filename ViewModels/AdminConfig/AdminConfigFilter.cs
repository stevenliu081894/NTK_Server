using NTKServer.Internal;

namespace NTKServer.ViewModels.AdminConfig
{
    public class AdminConfigFilter
    {
        /// <summary> name: 键值 </summary>
        [Where("LIKE", "admin_config.name")]
        public string? name { get; set; }

        /// <summary> title: 标题 </summary>
        [Where("LIKE", "admin_config.title")]
        public string? title { get; set; }

    }
}
