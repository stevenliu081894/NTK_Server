using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.HisRequestRenew;

namespace NTKServer.Business
{
    public class HisRequestRenewBiz
    {
        #region CRUD
		public static List<HisRequestRenewList> GetHisRequestRenewList(HisRequestRenewFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return BorrowRequestService.FindHisRequestRenewList(whereSql)?
                .Select(hisRequestRenew => PublicTool.convertUtcToLocalTime(hisRequestRenew)).ToList(); ;
        }

        public static BorrowRequestDto Get(int pk)
        {
            return BorrowRequestService.Find(pk);
        }

        public static void PostCreate(BorrowRequestDto req)
        {
            if (BorrowRequestService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowRequestDto req)
        {
            if( BorrowRequestService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowRequestService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
