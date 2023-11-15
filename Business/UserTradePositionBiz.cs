using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.UserTradePosition;

namespace NTKServer.Business
{
    public class UserTradePositionBiz
    {
        #region CRUD
		public static List<UserTradePositionList> GetUserTradePositionList(string subAccount, UserTradePositionFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must( $"trade_position.sub_account = '{subAccount}' ");
            
            return TradePositionService.FindUserTradePositionList(whereSql)?
                .Select(userTradePosition => PublicTool.convertUtcToLocalTime(userTradePosition)).ToList();
        }

        public static TradePositionDto Get(string sub_account)
        {
            return TradePositionService.Find(sub_account);
        }

        public static void PostCreate(TradePositionDto req)
        {
            if (TradePositionService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(TradePositionDto req)
        {
            if( TradePositionService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(string sub_account, string stock_code)
        {
            TradePositionService.Remove(sub_account, stock_code);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
