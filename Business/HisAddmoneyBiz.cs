using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.HisAddmoney;

namespace NTKServer.Business
{
    public class HisAddmoneyBiz
    {
        #region CRUD
		public static List<HisAddmoneyList> GetHisAddmoneyList(HisAddmoneyFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return BorrowAddmoneyService.FindHisAddmoneyList(whereSql)
                .Select(hisAddmoney => PublicTool.convertUtcToLocalTime(hisAddmoney)).ToList();
        }

        public static BorrowAddmoneyDto Get(int pk)
        {
            return BorrowAddmoneyService.Find(pk);
        }

        public static void PostCreate(BorrowAddmoneyDto req)
        {
            if (BorrowAddmoneyService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowAddmoneyDto req)
        {
            if( BorrowAddmoneyService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowAddmoneyService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		

        public static HisAddmoneyEditVm GetEditVm(int pk)
        {
            return BorrowAddmoneyService.FindHisAddmoneyEditVm(pk);
        }

		#endregion		
	}
}
