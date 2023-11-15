using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.TradePosition;

namespace NTKServer.Business
{
    public class TradePositionBiz
    {
        #region CRUD
		public static List<TradePositionList> GetTradePositionList(TradePositionFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return TradePositionService.FindTradePositionList(whereSql)?
                .Select(tradePosition => PublicTool.convertUtcToLocalTime(tradePosition)).ToList();
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
