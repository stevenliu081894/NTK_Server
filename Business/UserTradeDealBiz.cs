using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.TradePosition;
using NTKServer.ViewModels.UserTradeDeal;

namespace NTKServer.Business
{
    public class UserTradeDealBiz
    {
        #region CRUD
		public static List<UserTradeDealList> GetUserTradeDealList(string subAccount, UserTradeDealFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must($"trade_deal.sub_account = '{subAccount}' ")
				.Must($"DATE(`create_datetime`) >= UTC_DATE");
			return TradeDealService.FindUserTradeDealList(whereSql)?
                .Select(userTradeDeal => PublicTool.convertUtcToLocalTime(userTradeDeal)).ToList();
        }

        public static TradeDealDto Get(int pk)
        {
            return TradeDealService.Find(pk);
        }

        public static void PostCreate(TradeDealDto req)
        {
            if (TradeDealService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(TradeDealDto req)
        {
            if( TradeDealService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            TradeDealService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
