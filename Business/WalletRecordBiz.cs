using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.WalletRecord;

namespace NTKServer.Business
{
    public class WalletRecordBiz
    {
        #region CRUD
		public static List<WalletRecordList> GetWalletRecordList(int id)
        {            
            return WalletRecordService.FindWalletRecordList(id)?
                .Select(walletRecord => PublicTool.convertUtcToLocalTime(walletRecord)).ToList(); ;
        }

        public static WalletRecordDto Get(int pk)
        {
            return WalletRecordService.Find(pk);
        }

        public static void PostCreate(WalletRecordDto req)
        {
            if (WalletRecordService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(WalletRecordDto req)
        {
            if( WalletRecordService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            WalletRecordService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
