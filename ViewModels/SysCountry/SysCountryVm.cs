using X.PagedList;

namespace NTKServer.ViewModels.SysCountry
{
    public class SysCountryVm
    {
        public SysCountryFilter filter { get; set; }
        public IPagedList<SysCountryList> list { get; set; }
    }
}
