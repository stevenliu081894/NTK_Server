using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.AdminBank;

namespace NTKServer.Business
{
    public class AdminBankBiz
    {
        #region CRUD
		public static List<AdminBankList> GetAdminBankList(AdminBankFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return AdminBankService.FindAdminBankList(whereSql);
        }

        public static AdminBankDto Get(int pk)
        {
            return AdminBankService.Find(pk);
        }

        public static void PostCreate(AdminBankDto req)
        {
            if (AdminBankService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(AdminBankDto req)
        {
            if( AdminBankService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            AdminBankService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
