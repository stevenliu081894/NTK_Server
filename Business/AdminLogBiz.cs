using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.AdminLog;

namespace NTKServer.Business
{
    public class AdminLogBiz
    {
        #region CRUD
		public static List<AdminLogList> GetAdminLogList(AdminLogFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return AdminLogService.FindAdminLogList(whereSql)?
                .Select(adminlog => PublicTool.convertUtcToLocalTime(adminlog)).ToList();
        }

        public static AdminLogDto Get(int pk)
        {
            return AdminLogService.Find(pk);
        }

        public static void PostCreate(AdminLogDto req)
        {
            if (AdminLogService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(AdminLogDto req)
        {
            if( AdminLogService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            AdminLogService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
