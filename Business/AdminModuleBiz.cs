using DB.Services;
using Models.Dto;
using NTKServer.Tool;
using NTKServer.ViewModels.AdminUser;

namespace NTKServer.Business
{
    public class AdminModuleBiz
    {
        public static AdminModuleDto Get(int pk)
        {
            return AdminModuleService.Find(pk);
        }

        public static List<AdminModuleDto> GetEnabledList()
        {
            return AdminModuleService.FindAll(1);
        }

        //public static AdminModuleDto? GetByAdminUser(int admin_user_pk)
        //{
        //    return AdminModuleService.FindByAdminUser(admin_user_pk);
        //}
    }
}
