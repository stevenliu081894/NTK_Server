using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.AdminLogin;
using NTKServer.Libs;

namespace NTKServer.Business
{
    public class AdminLoginBiz
    {
        #region CRUD
		public static List<AdminLoginList> GetAdminLoginList(AdminLoginFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return AdminLoginService.FindAdminLoginList(whereSql)?
                .Select(adminLogin => PublicTool.convertUtcToLocalTime(adminLogin)).ToList();
        }

        public static AdminLoginDto Get(int pk)
        {
            return AdminLoginService.Find(pk);
        }

        public static void PostCreate(AdminLoginDto req)
        {
            if (AdminLoginService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(AdminLoginDto req)
        {
            if( AdminLoginService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            AdminLoginService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
