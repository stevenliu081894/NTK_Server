using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.EndWalletWithdraw;

namespace NTKServer.Business
{
    public class EndWalletWithdrawBiz
    {
        #region CRUD
		public static List<EndWalletWithdrawList> GetEndWalletWithdrawList(EndWalletWithdrawFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return WalletWithdrawService.FindEndWalletWithdrawList(whereSql)?
                .Select(endWalletWithdraw => PublicTool.convertUtcToLocalTime(endWalletWithdraw)).ToList();
        }

        public static WalletWithdrawDto Get(int pk)
        {
            return WalletWithdrawService.Find(pk);
        }

        public static void PostCreate(WalletWithdrawDto req)
        {
            if (WalletWithdrawService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(WalletWithdrawDto req)
        {
            if( WalletWithdrawService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            WalletWithdrawService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		

        public static EndWalletWithdrawEditVm GetEditVm(int pk)
        {
            return WalletWithdrawService.FindEndWalletWithdrawEditVm(pk);
        }

		#endregion		
	}
}
