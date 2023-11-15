using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.StatWalletRecordDetail;

namespace NTKServer.Business
{
    public class StatWalletRecordDetailBiz
    {
        #region CRUD
		public static List<StatWalletRecordDetailList> GetStatWalletRecordDetailList(StatWalletRecordDetailFilter? filter, string lang)
        {
            string whereSql = SqlTool.Build(filter).Must($"wallet_template.lang = '{lang}'");
            return WalletRecordService.FindStatWalletRecordDetailList(whereSql);
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
