using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.TradeCancel;

namespace NTKServer.Business
{
    public class TradeCancelBiz
    {
        #region CRUD
		public static List<TradeCancelList> GetTradeCancelList(string subAccount, TradeCancelFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must($"sub_account = '{subAccount}'")
				.Must($"DATE(`cancel_datetime`) >= UTC_DATE");
			return TradeCancelService.FindTradeCancelList(whereSql)?
                .Select(tradeCancel => PublicTool.convertUtcToLocalTime(tradeCancel)).ToList();
        }

        public static TradeCancelDto Get(int pk)
        {
            return TradeCancelService.Find(pk);
        }

        public static void PostCreate(TradeCancelDto req)
        {
            if (TradeCancelService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(TradeCancelDto req)
        {
            if( TradeCancelService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            TradeCancelService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
