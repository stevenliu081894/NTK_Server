using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class RecommendRewardSummaryService
    {
        #region Dto

        public static RecommendRewardSummaryDto Find(int pk)
        {
            string sql = @"SELECT * FROM `recommend_reward_summary` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<RecommendRewardSummaryDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardSummaryService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RecommendRewardSummaryDto> FindAll()
        {
            string sql = @"SELECT * FROM `recommend_reward_summary`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RecommendRewardSummaryDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardSummaryService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(RecommendRewardSummaryDto source)
        {
            string sql = @"INSERT INTO `recommend_reward_summary` (
				`member_fk`, `recommend_reward_fk`, `layer`, `year`, `month`, `monthly_members`, `monthly_borrow_fee`, `monthly_reward`)
				VALUES (@member_fk, @recommend_reward_fk, @layer, @year, @month, @monthly_members, @monthly_borrow_fee, @monthly_reward);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardSummaryService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(RecommendRewardSummaryDto model)
        {
            string sql = @"UPDATE `recommend_reward_summary` SET 
				`member_fk` = @member_fk,
				`recommend_reward_fk` = @recommend_reward_fk,
				`layer` = @layer,
				`year` = @year,
				`month` = @month,
				`monthly_members` = @monthly_members,
				`monthly_borrow_fee` = @monthly_borrow_fee,
				`monthly_reward` = @monthly_reward
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardSummaryService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `recommend_reward_summary` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardSummaryService][Remove]" + ex.Message);
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
