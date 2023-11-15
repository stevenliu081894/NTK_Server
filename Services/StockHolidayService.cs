using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.StockHoliday;

namespace DB.Services
{
    public class StockHolidayService
    {
        #region Dto

        public static StockHolidayDto Find(int pk)
        {
            string sql = @"SELECT * FROM `stock_holiday` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<StockHolidayDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockHolidayService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<StockHolidayDto> FindAll()
        {
            string sql = @"SELECT * FROM `stock_holiday`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockHolidayDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockHolidayService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(StockHolidayDto source)
        {
            string sql = @"INSERT INTO `stock_holiday` (
				`market`, `name`, `year`, `date`, `is_allday`, `open`, `close`)
				VALUES (@market, @name, @year, @date, @is_allday, @open, @close);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockHolidayService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(StockHolidayDto model)
        {
            string sql = @"UPDATE `stock_holiday` SET 
				`market` = @market,
				`name` = @name,
				`year` = @year,
				`date` = @date,
				`is_allday` = @is_allday,
				`open` = @open,
				`close` = @close
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockHolidayService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `stock_holiday` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockHolidayService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<StockHolidayList> FindStockHolidayList(string whereSql="")
        {
            string sql = @$"SELECT stock_holiday.pk, stock_holiday.market, stock_holiday.name, stock_holiday.year, stock_holiday.date, stock_holiday.is_allday, stock_holiday.open, stock_holiday.close FROM `stock_holiday`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StockHolidayList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[StockHolidayService][FindStockHolidayList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
