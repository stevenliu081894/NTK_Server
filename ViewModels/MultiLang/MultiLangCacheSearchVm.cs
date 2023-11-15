using NTKServer.Internal;
using NTKServer.ViewModels.SysMarket;
using X.PagedList;

namespace NTKServer.ViewModels.MultiLang
{
    public class MultiLangCacheSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public MultiLangCacheSearchFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<MultiLangCacheSearchList> list { get; set; }

        /// <summary> 分页 </summary>
        public int page { get; set; } = 1;

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }
    }

    public class MultiLangCacheSearchFilter
    {
        /// <summary> key: pk </summary>
        [Where("=", "key")]
        public string? key { get; set; }
        /// <summary> lang: pk </summary>
        [Where("=", "lang")]
        public string? lang { get; set; }
    }

    public class MultiLangCacheSearchList
    { 
        public string lang { get; set; }
        /// <summary> Key </summary>
        public string key { get; set; }
        /// <summary>说明 </summary>
        public string description { get; set; }
        /// <summary> 翻译文 </summary>
        public string translation { get; set; }
    }

    public class MultiLangCacheEditVm
    {
        /// <summary> Key </summary>
        public string key { get; set; }
        public string lang { get; set; }
        public string description { get; set; }
        /// <summary> 翻译文 </summary>
        public string translationKey { get; set; }
        /// <summary> 翻译文 </summary>
        public string translation { get; set; }
    }
}
