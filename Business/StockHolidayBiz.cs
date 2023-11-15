using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.StockHoliday;

namespace NTKServer.Business
{
    public class StockHolidayBiz
    {
        #region CRUD
		public static List<StockHolidayList> GetStockHolidayList(StockHolidayFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return StockHolidayService.FindStockHolidayList(whereSql)?
                .Select(stockHoliday => PublicTool.convertUtcToLocalTime(stockHoliday)).ToList();
        }

        public static StockHolidayDto Get(int pk)
        {
            return StockHolidayService.Find(pk);
        }

        public static void PostCreate(StockHolidayDto req)
        {
            if (StockHolidayService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(StockHolidayDto req)
        {
            if( StockHolidayService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            StockHolidayService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
