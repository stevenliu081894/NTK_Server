namespace NTKServer.ViewModels.AdminRole
{
    public class AdminRoleList
    {
        /// <summary> pk:角色pk </summary>
        public int pk { get; set; }

        /// <summary> name:角色名称 </summary>
        public string name { get; set; }

        /// <summary> description:角色描述 </summary>
        public string description { get; set; }

        /// <summary> 状态 </summary>
        public int status { get; set; }
    }
}
