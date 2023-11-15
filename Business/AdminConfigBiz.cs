using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.AdminConfig;

namespace NTKServer.Business
{
    public class AdminConfigBiz
    {
        #region CRUD
		public static List<AdminConfigList> GetAdminConfigList(AdminConfigFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return AdminConfigService.FindAdminConfigList(whereSql);
        }

        public static AdminConfigDto Get(string name)
        {
            return AdminConfigService.Find(name);
        }

        public static void PostCreate(AdminConfigDto req)
        {
            if (AdminConfigService.Insert(req) == 0)
            {
                ConfigLib.Reset();

                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(AdminConfigDto req)
        {
            if( AdminConfigService.UpdateConfig(req) == 0)
            {
                ConfigLib.Reset();

                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(string name)
        {
            AdminConfigService.Remove(name);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
