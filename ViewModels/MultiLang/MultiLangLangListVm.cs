using NTKServer.ViewModels.SysMarket;
using X.PagedList;

namespace NTKServer.ViewModels.MultiLang
{
    public class MultiLangLangSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public SysMarketFilter filter { get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<MultiLangLanghList> list { get; set; }

        /// <summary> 分页 </summary>
        public int page { get; set; } = 1;

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }
    }
}
