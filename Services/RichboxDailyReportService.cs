using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class RichboxDailyReportService
    {
        #region Dto

        public static RichboxDailyReportDto Find(string report_date)
        {
            string sql = @"SELECT * FROM `richbox_daily_report` WHERE `report_date` = @report_date";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {report_date});
                return conn.QueryFirstOrDefault<RichboxDailyReportDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxDailyReportService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RichboxDailyReportDto> FindAll()
        {
            string sql = @"SELECT * FROM `richbox_daily_report`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RichboxDailyReportDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxDailyReportService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(RichboxDailyReportDto model)
        {
			string sql = @"INSERT INTO `richbox_daily_report` (
				`report_date`, `total_invest`, `total_profit`, `total_input`, `total_output`, `total_interest`)
				VALUES (@report_date, @total_invest, @total_profit, @total_input, @total_output, @total_interest); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxDailyReportService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(RichboxDailyReportDto model)
        {
            string sql = @"UPDATE `richbox_daily_report` SET 
				`total_invest` = @total_invest,
				`total_profit` = @total_profit,
				`total_input` = @total_input,
				`total_output` = @total_output,
				`total_interest` = @total_interest
				 WHERE `report_date` = @report_date";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxDailyReportService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string report_date)
        {
            string sql = @"DELETE FROM `richbox_daily_report` WHERE `report_date` = @report_date";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { report_date });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxDailyReportService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

        #endregion

        #region Other

        #endregion
    }
}
