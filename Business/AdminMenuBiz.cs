using DB.Services;
using Models.Dto;
using NTKServer.Cache;
using NTKServer.Tool;
using NTKServer.ViewModels.Menu;

namespace NTKServer.Business
{
    public class AdminMenuBiz
    {       
        /// <summary>
        /// 依照角色取得菜单
        /// </summary>
        public static List<MainMenuVm> GetMenuByRole(int role)
        {
            CacheQuery.SelectDB(CacheEnum.admin);
            return CacheQuery.HashGet<List<MainMenuVm>>("AdminMenu", role.ToString());
        }

        #region 更新快取

        /// <summary>
        /// 更新某角色快取
        /// </summary>
        public static void UpdateMenu(int role)
        {                       
            AdminRoleDto item = AdminRoleService.Find(role);
            if (item != null)
            {
                List<AdminMenuDto> fullMenu = AdminMenuService.FindAllByFirstMenu();
                Dictionary<int, int[]> dic = PublicTool.FromJson<Dictionary<int, int[]>>(item.admin_menu);
                var menu = GetMenuFromDb(dic, fullMenu);

                CacheQuery.SelectDB(CacheEnum.admin);
                CacheQuery.HashSet("AdminMenu", role.ToString(), menu);
            }
        }
        #endregion

        #region 選單快取

        /// <summary>
        /// 预先建好菜单, 避免每次都要拉数据库重新组合
        /// </summary>
        public static void BuildMenu()
        {
            CacheQuery.SelectDB(CacheEnum.admin);
            if (CacheQuery.HashExists("AdminMenu", "1") == true) return;

            List<AdminRoleDto> roles = AdminRoleService.FindAll();
            if (roles != null && roles.Count > 0)
            {
                List<AdminMenuDto> fullMenu = AdminMenuService.FindAllByFirstMenu();

                foreach (var item in roles)
                {
                    int id = item.pk;

                    Dictionary<int, int[]> dic = PublicTool.FromJson<Dictionary<int, int[]>>(item.admin_menu);
                    var menu = GetMenuFromDb(dic, fullMenu);

                    CacheQuery.HashSet("AdminMenu", id.ToString(), menu);
                }
            }
        }

        public static List<MainMenuVm> GetMenuFromDb(Dictionary<int, int[]> dic, List<AdminMenuDto> fullMenu)
        {
            List<MainMenuVm> mainMenu = new();
            List<AdminModuleDto> modules = AdminModuleService.FindAll();

            foreach (var item in dic.Keys)
            {
                AdminModuleDto? data = modules.Where(x=>x.pk == item).FirstOrDefault();
                if (data != null)
                {
                    MainMenuVm menuViewModel = new()
                    {
                        Id = item,
                        Parent = 0,
                        Url = "",
                        Active = "",
                        IconClass = data.icon,
                        Title = data.title,
                        Child = GetMenuByModel(item, dic[item], fullMenu)
                    };
                    mainMenu.Add(menuViewModel);
                }
            }

            mainMenu.Add(new MainMenuVm { Id = 9999, Parent = 0, Url = "/Login/index", Active = "", IconClass = "fa fa-fw fa-sign-out", Title = "登出" });
            return mainMenu;
        }

        public static List<MainMenuVm> GetMenuByModel(int modelid, int[] numbers, List<AdminMenuDto> fullMenu)
        {
            List<MainMenuVm> menu = new();
            // 权限阵列
            if(numbers == null || numbers.Length == 0) return menu;
            
            List<AdminMenuDto> list = fullMenu.Where(d=>d.admin_module_fk == modelid).OrderBy(d=>d.sort).ToList();
  
            if (list != null)
            {
                foreach (var item in list)
                {
                    // 只显示拥有权限的选单
                    if (Array.IndexOf(numbers, item.pk) > -1)
                    {
                        MainMenuVm menuViewModel = new()
                        {
                            Id = item.pk,
                            Parent = item.parent,
                            Url = item.url_value,
                            Active = "",
                            IconClass = "fa fa-circle nav-icon",
                            Title = item.title
                        };
                        menu.Add(menuViewModel);
                    }
                }
            }
            return menu;
        }

        #endregion

        public static AdminMenuDto GetMenuByPK(int id)
        {
            return AdminMenuService.Find(id);
        }
    }
}
