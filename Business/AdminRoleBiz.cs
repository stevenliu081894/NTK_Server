using DB.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Models.Tree;
using NTKServer.Tool;
using NTKServer.ViewModels.AdminRole;
using NTKServer.ViewModels.MultiLang;

namespace NTKServer.Business
{
    public class AdminRoleBiz
    {
		#region CRUD

		public static List<SelectListItem> FindSelectList()
		{
            var roles= AdminRoleService.FindAll();
            var list=new List<SelectListItem>();
            foreach (var role in roles)
            {
                list.Add(new SelectListItem()
                {
                    Value = $"{role.pk}",
                    Text = role.name
                });
            }
            return list;
		}

		public static List<AdminRoleList> GetAdminRoleList()
        {
            return AdminRoleService.FindAdminRoleList();
        }

        public static AdminRoleDto Get(int pk)
        {
            return AdminRoleService.Find(pk);
        }

        public static AdminRoleList GetAdminRole(int pk)
        {
            return AdminRoleService.FindAdminRole(pk);
        }

        public static void PostCreate(AdminRoleDto req)
        {
            if (AdminRoleService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(AdminRoleDto req)
        {
            if( AdminRoleService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            AdminRoleService.Remove(pk);
        }

        #endregion

        #region 驗證權限
        public static bool VerifyPower(string router)
        {
            try
            {

                //AdminRoleDto role = AdminRoleService.Find(roleId);
                //Dictionary<int, string> dic = PublicTool.FromJson<Dictionary<int, string>>(role.admin_menu);
                //if (dic != null)
                //{
                //    string powerStr = dic[modelId];
                //    int[] list = PublicTool.FromJson<int[]>(powerStr);
                //    if (Array.IndexOf(list, menuId) >= 0)
                //    {
                //        return true;
                //    }
                //}
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /// <summary>
        /// 驗證角色權限
        /// </summary>
        /// <param name="menuId">權限編號 admin_menu.pk</param>
        /// <param name="modelId">模組編號 admin_menu.admin_module_fk</param>
        /// <param name="roleId">角色編號 admin_user.role</param>
        /// <returns></returns>
        public static bool VerifyPower(int menuId, int modelId, int roleId)
        {
            try
            {
                AdminRoleDto role = AdminRoleService.Find(roleId);
                Dictionary<int, int[]> dic = PublicTool.FromJson<Dictionary<int, int[]>>(role.admin_menu);
                if (dic != null)
                {
                    int[] list = dic[modelId];
                    if (Array.IndexOf(list, menuId) >= 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        #endregion

        #region 建立權限樹

        private static PowerNode CreatePowerNode(int id, bool isSelect, string name, string icon)
        {
            PowerNode node = new()
            {
                id = id,
                text = name,
                icon = "fa fa-lg " + icon,
                state = new NodeState(isSelect),
                children = new()
            };

            return node;
        }

        /// <summary>
        /// 建立 tree 第一層
        /// </summary>
        private static void BuildFirstNode(PowerNode parent, List<AdminMenuDto> fullMenu, int[] power)
        {
            List<AdminMenuDto> list = fullMenu.Where(d => d.admin_module_fk == parent.id - 10000 && d.parent == d.pk).OrderBy(d => d.sort).ToList();
            if(list != null && list.Count > 0) 
            {
                foreach(AdminMenuDto menu in list) 
                {
                    bool chk = GetPower(menu.pk, power);
                    var node = CreatePowerNode(menu.pk, chk, menu.title, "fa-angle-right");
                    BuildSecondNode(node, fullMenu, power);
                    parent.children.Add(node);
                }
            }
        }

        /// <summary>
        /// 建立 tree 第二層
        /// </summary>
        private static void BuildSecondNode(PowerNode parent, List<AdminMenuDto> fullMenu, int[] power)
        {
            List<AdminMenuDto> list = fullMenu.Where( d => d.parent == parent.id && d.pk != d.parent ).OrderBy(d =>d.sort).ToList();
            if (list != null && list.Count > 0)
            {
                foreach (AdminMenuDto menu in list)
                {
                    bool chk = GetPower(menu.pk, power);
                    var node = CreatePowerNode(menu.pk, chk, menu.title, "fa-cog");
                    parent.children.Add(node);
                }
            }
        }

        private static int[] ToPowerArray(string json)
        {
            int[] result = Array.Empty<int>();
            if (json == null || string.IsNullOrEmpty(json) == true) return result;
            var data = PublicTool.FromJson<Dictionary<int, int[]>>(json);
            foreach ( var item in data)
            {
                int[] numbers = item.Value;
                if (numbers != null && numbers.Length > 0)
                {
                    int old_length = result.Length;
                    Array.Resize<int>(ref result, result.Length + numbers.Length);
                    Array.Copy(numbers, 0, result, old_length, numbers.Length);
                }
            }

            return result;
        }

        private static bool GetPower(int id, int[] power)
        {
            if(power == null || power.Length == 0 ) return false;

            try
            {
                if (Array.IndexOf(power, id) > -1) return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 建立角色權限樹
        /// </summary>
        public static List<PowerNode> AdminMenuToTree(string admin_menu)
        {
            var root = CreatePowerNode(0, false, "角色權限", "fa-home");
            List<PowerNode> tree = new() { root };

            try
            {
                int[] power = ToPowerArray(admin_menu);
                Dictionary<int, int[]> original_power = PublicTool.FromJson<Dictionary<int, int[]>>(admin_menu);

                List<AdminModuleDto> module = AdminModuleService.FindAll();
                List<AdminMenuDto> fullMenu = AdminMenuService.FindAll();

                foreach (var module_item in module)
                {
                    int id = module_item.pk;
                    var node = CreatePowerNode(id+10000, false, module_item.title, "fa-folder");
                    BuildFirstNode(node, fullMenu, power);
                    root.children.Add(node);
                }
            }
            catch (Exception)
            {
                throw new AppException("生成jtree異常");
            }

            return tree;
        }


        /// <summary>
        /// jTree 選取的資料轉為 admin_menu 需要的資料格式
        /// </summary>
        public static string TreeToAdminMenu(string tree)
        {
            try
            {
                if (string.IsNullOrEmpty(tree) == true) return ("{}");

                int[] power = PublicTool.FromJson<int[]>(tree);

                Dictionary<int, int[]> dic = new();

                List <AdminModuleDto> module = AdminModuleService.FindAll();
                List<AdminMenuDto> fullMenu = AdminMenuService.FindAll();

                foreach (var module_item in module)
                {
                    int id = module_item.pk;
                    var value = fullMenu.Where(p => p.admin_module_fk == id).ToArray();

                    if (value.Length > 0)
                    {
                        var sameArr = value.Select(p => p.pk).Intersect(power).ToArray();
                        if (sameArr.Length > 0)
                        {
                            dic.Add(id, sameArr);
                        }
                    }
                }

                return PublicTool.ToJson(dic);
            }
            catch (Exception)
            {
                throw new AppException("[AdminRoleBiz][TreeToAdminMenu]無法解析字串內容");
            }
        }

        #endregion
    }
}
