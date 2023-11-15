using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.AdminUser;

namespace NTKServer.Business
{
    public class AdminUserBiz
    {
        #region CRUD
		public static List<AdminUserList> GetAdminUserList(AdminUserFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return AdminUserService.FindAdminUserList(whereSql)?
                .Select(adminUser => PublicTool.convertUtcToLocalTime(adminUser)).ToList();
        }

        public static AdminUserDto Get(int pk)
        {
            return AdminUserService.Find(pk);
        }

        public static void PostCreate(AdminUserDto req)
        {
            if (AdminUserService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(AdminUserDto req)
        {
            if( AdminUserService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            AdminUserService.Remove(pk);
        }

        #endregion

        #region ViewModel		
        public static AdminUserDto GetByAccount(string account)
        {
            return AdminUserService.FindByAccount(account);
        }

        #endregion
    }
}
