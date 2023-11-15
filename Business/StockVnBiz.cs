using DB.Services;
using Models.Dto;
using Models.ViewModels;
using NTKServer.Cache;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.StockVn;

namespace NTKServer.Business
{
    public class StockVnBiz
    {
        #region CRUD
		public static List<StockVnList> GetStockVnList(StockVnFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return StockVnService.FindStockVnList(whereSql)?
                .Select(reviewMember => PublicTool.convertUtcToLocalTime(reviewMember)).ToList();
        }

        public static StockVnDto Get(string stock_code)
        {
            return StockVnService.Find(stock_code);
        }

        public static void PostCreate(StockVnDto req)
        {
            if (StockVnService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(StockVnDto req)
        {
            if( StockVnService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
            else
            {
                // Stock更新後，重新整理 Redis 的 @stock_info
                var info_dict = StockVnService.FindEnableList()
                    .ToDictionary(
                        x => x.stock_code,
                        x => new StockInfo
                        {
                            stock_code = x.stock_code,
                            stock_name = x.stock_name,
                        }
                    );

                // 寫入 Redis
                CacheQuery.SelectDB(CacheEnum.VnStockData);
                CacheQuery.StringSet("@stock_info", info_dict);
            }
        }

        public static void Delete(string stock_code)
        {
            StockVnService.Remove(stock_code);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
