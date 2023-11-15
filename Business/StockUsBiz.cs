using DB.Services;
using Models.Dto;
using Models.ViewModels;
using NTKServer.Cache;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.StockUs;

namespace NTKServer.Business
{
    public class StockUsBiz
    {
        #region CRUD
		public static List<StockUsList> GetStockUsList(StockUsFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return StockUsService.FindStockUsList(whereSql)?
                .Select(stockUs => PublicTool.convertUtcToLocalTime(stockUs)).ToList();
        }

        public static StockUsDto Get(string stock_code)
        {
            return StockUsService.Find(stock_code);
        }

        public static void PostCreate(StockUsDto req)
        {
            if (StockUsService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(StockUsDto req)
        {
            if( StockUsService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
            else
            {
                // Stock更新後，重新整理 Redis 的 @stock_info
                var info_dict = StockUsService.FindEnableList()
                    .ToDictionary(
                        x => x.stock_code,
                        x => new StockInfo
                        {
                            stock_code = x.stock_code,
                            stock_name = x.stock_name,
                        }
                    );

                // 寫入 Redis
                CacheQuery.SelectDB(CacheEnum.UsStockData);
                CacheQuery.StringSet("@stock_info", info_dict);
            }
        }

        public static void Delete(string stock_code)
        {
            StockUsService.Remove(stock_code);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
