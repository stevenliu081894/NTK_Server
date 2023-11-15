using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.RecommendReward;

namespace DB.Services
{
    public class RecommendRewardService
    {
        #region Dto

        public static RecommendRewardDto Find(int pk)
        {
            string sql = @"SELECT * FROM `recommend_reward` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<RecommendRewardDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RecommendRewardDto> FindAll()
        {
            string sql = @"SELECT * FROM `recommend_reward`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RecommendRewardDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(RecommendRewardDto source)
        {
            string sql = @"INSERT INTO `recommend_reward` (
				`member_fk`, `year`, `month`, `currency`, `total_reward`, `state`, `withdraw`, `paydate`, `note`, `create_time`)
				VALUES (@member_fk, @year, @month, @currency, @total_reward, @state, @withdraw, @paydate, @note, @create_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(RecommendRewardDto model)
        {
            string sql = @"UPDATE `recommend_reward` SET 
				`member_fk` = @member_fk,
				`year` = @year,
				`month` = @month,
				`currency` = @currency,
				`total_reward` = @total_reward,
				`state` = @state,
				`withdraw` = @withdraw,
				`paydate` = @paydate,
				`note` = @note,
				`create_time` = @create_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `recommend_reward` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<RecommendRewardList> FindRecommendRewardList(string whereSql="")
        {
            string sql = @$"SELECT recommend_reward.pk, member.account, member.nickname, recommend_reward.year, recommend_reward.month, 
                recommend_reward.currency, recommend_reward.total_reward, recommend_reward.state, recommend_reward.withdraw, 
                recommend_reward.paydate, recommend_reward.create_time 
                FROM `recommend_reward`
                INNER JOIN `member` on member.pk = recommend_reward.member_fk
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RecommendRewardList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardService][FindRecommendRewardList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
