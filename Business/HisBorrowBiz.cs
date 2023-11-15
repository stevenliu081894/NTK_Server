using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.HisBorrow;

namespace NTKServer.Business
{
    public class HisBorrowBiz
    {
        #region CRUD
		public static List<HisBorrowList> GetHisBorrowList(HisBorrowFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return BorrowService.FindHisBorrowList(whereSql)?
                .Select(exchangeRate => PublicTool.convertUtcToLocalTime(exchangeRate)).ToList();
        }

        public static BorrowDto Get(int pk)
        {
            return BorrowService.Find(pk);
        }

        public static void PostCreate(BorrowDto req)
        {
            if (BorrowService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowDto req)
        {
            if( BorrowService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		

        public static HisBorrowEditVm GetDetailVm(int pk)
        {
            return BorrowService.FindHisBorrowEditVm(pk);
        }

		#endregion		
	}
}
