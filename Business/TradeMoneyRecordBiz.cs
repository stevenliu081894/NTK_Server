using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.TradeMoneyRecord;

namespace NTKServer.Business
{
    public class TradeMoneyRecordBiz
    {
        #region CRUD
		public static List<TradeMoneyRecordList> GetTradeMoneyRecordList(TradeMoneyRecordFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must($"sub_account = '{filter.sub_account}'");
            return TradeMoneyRecordService.FindTradeMoneyRecordList(whereSql)?
                .Select(tradeMoneyRecord => PublicTool.convertUtcToLocalTime(tradeMoneyRecord)).ToList();
        }

        public static TradeMoneyRecordDto Get(int pk)
        {
            return TradeMoneyRecordService.Find(pk);
        }

        public static void PostCreate(TradeMoneyRecordDto req)
        {
            if (TradeMoneyRecordService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(TradeMoneyRecordDto req)
        {
            if( TradeMoneyRecordService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            TradeMoneyRecordService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
