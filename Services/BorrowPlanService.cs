using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.BorrowPlan;

namespace DB.Services
{
    public class BorrowPlanService
    {
        #region Dto

        public static BorrowPlanDto Find(int pk)
        {
            string sql = @"SELECT * FROM `borrow_plan` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<BorrowPlanDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowPlanService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<BorrowPlanDto> FindAll()
        {
            string sql = @"SELECT * FROM `borrow_plan`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowPlanDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowPlanService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(BorrowPlanDto source)
        {
            string sql = @"INSERT INTO `borrow_plan` (
				`enable`, `borrow_type`, `market`, `name`, `lang`, `rate`, `warning_line`, `break_line`, `max_proporting`, `renewal`, `use_time`, `money_range_min`, `money_range_max`, `money_range_increase`, `fastbtn`, `slogan`, `note`, `unique_set`)
				VALUES (@enable, @borrow_type, @market, @name, @lang, @rate, @warning_line, @break_line, @max_proporting, @renewal, @use_time, @money_range_min, @money_range_max, @money_range_increase, @fastbtn, @slogan, @note, @unique_set);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowPlanService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(BorrowPlanDto model)
        {
            string sql = @"UPDATE `borrow_plan` SET 
				`enable` = @enable,
				`borrow_type` = @borrow_type,
				`market` = @market,
				`name` = @name,
				`lang` = @lang,
				`rate` = @rate,
				`warning_line` = @warning_line,
				`break_line` = @break_line,
				`max_proporting` = @max_proporting,
				`renewal` = @renewal,
				`use_time` = @use_time,
				`money_range_min` = @money_range_min,
				`money_range_max` = @money_range_max,
				`money_range_increase` = @money_range_increase,
				`fastbtn` = @fastbtn,
				`slogan` = @slogan,
				`note` = @note,
				`unique_set` = @unique_set
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowPlanService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `borrow_plan` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowPlanService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<BorrowPlanList> FindBorrowPlanList(string whereSql="")
        {
            string sql = @$"SELECT borrow_plan.pk, borrow_plan.market, borrow_plan.enable, borrow_plan.borrow_type, borrow_plan.name 
                FROM `borrow_plan`{whereSql} order by market, borrow_type ";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowPlanList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[Service][FindBorrowPlanList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
