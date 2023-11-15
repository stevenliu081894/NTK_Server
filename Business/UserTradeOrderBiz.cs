using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.UserTradeOrder;

namespace NTKServer.Business
{
    public class UserTradeOrderBiz
    {
        #region CRUD
		public static List<UserTradeOrderList> GetUserTradeOrderList(string subAccount, UserTradeOrderFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must($"trade_order.sub_account = '{subAccount}'")
                .Must($"DATE(`order_time`) >= UTC_DATE");
			return TradeOrderService.FindUserTradeOrderList(whereSql)?
                .Select(tradeOrder => PublicTool.convertUtcToLocalTime(tradeOrder)).ToList();
        }

        public static TradeOrderDto Get(int pk)
        {
            return TradeOrderService.Find(pk);
        }

        public static void PostCreate(TradeOrderDto req)
        {
            if (TradeOrderService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(TradeOrderDto req)
        {
            if( TradeOrderService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            TradeOrderService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
