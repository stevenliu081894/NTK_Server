using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.BorrowFee;

namespace NTKServer.Business
{
    public class BorrowFeeBiz
    {
        #region CRUD
		public static List<BorrowFeeList> GetBorrowFeeList(BorrowFeeFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return BorrowFeeService.FindBorrowFeeList(whereSql)?
                .Select(borrowFee => PublicTool.convertUtcToLocalTime(borrowFee)).ToList();
        }

        public static BorrowFeeDto Get(int pk)
        {
            return BorrowFeeService.Find(pk);
        }

        public static void PostCreate(BorrowFeeDto req)
        {
            if (BorrowFeeService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowFeeDto req)
        {
            if( BorrowFeeService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowFeeService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
