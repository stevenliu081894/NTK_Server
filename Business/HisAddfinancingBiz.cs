using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.HisAddfinancing;

namespace NTKServer.Business
{
    public class HisAddfinancingBiz
    {
        #region CRUD
		public static List<HisAddfinancingList> GetHisAddfinancingList(HisAddfinancingFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return BorrowAddfinancingService.FindHisAddfinancingList(whereSql)?
                .Select(endTradeAccount => PublicTool.convertUtcToLocalTime(endTradeAccount)).ToList();
        }

        public static BorrowAddfinancingDto Get(int pk)
        {
            return BorrowAddfinancingService.Find(pk);
        }

        public static void PostCreate(BorrowAddfinancingDto req)
        {
            if (BorrowAddfinancingService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowAddfinancingDto req)
        {
            if( BorrowAddfinancingService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowAddfinancingService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		

        public static HisAddfinancingEditVm GetEditVm(int pk)
        {
            return BorrowAddfinancingService.FindHisAddfinancingEditVm(pk);
        }

		#endregion		
	}
}
