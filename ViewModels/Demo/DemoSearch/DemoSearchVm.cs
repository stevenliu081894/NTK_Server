using X.PagedList;

namespace NTKServer.ViewModels.Demo
{
    public class DemoSearchVm
    {
        /// <summary> 搜寻条件 </summary>
        public DemoSearchFilter filter {  get; set; }

        /// <summary> 资料结果 </summary>
        public IPagedList<DemoSearchList> list { get; set; }

        /// <summary> 排序栏位名称 </summary>
        public string sort { get; set; }

        /// <summary> 正向排序還是逆向 </summary>
        public SortEnum dir { get; set; }
    }
}
