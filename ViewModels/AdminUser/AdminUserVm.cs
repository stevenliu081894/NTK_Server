using Microsoft.AspNetCore.Mvc.Rendering;
using NTKServer.ViewModels.MultiLang;
using X.PagedList;

namespace NTKServer.ViewModels.AdminUser
{
    public class AdminUserVm
    {
        public AdminUserFilter filter { get; set; }
        public IPagedList<AdminUserList> list { get; set; }
		public List<SelectListItem> roleDropdown { get; set; }

	}
}
