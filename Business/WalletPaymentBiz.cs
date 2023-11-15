using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.WalletPayment;

namespace NTKServer.Business
{
    public class WalletPaymentBiz
    {
        #region CRUD
		public static List<WalletPaymentList> GetWalletPaymentList(WalletPaymentFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return WalletPaymentService.FindWalletPaymentList(whereSql);
        }

        public static WalletPaymentDto Get(int pk)
        {
            return WalletPaymentService.Find(pk);
        }

        public static void PostCreate(WalletPaymentDto req)
        {
            if (WalletPaymentService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(WalletPaymentDto req)
        {
            if( WalletPaymentService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            WalletPaymentService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
