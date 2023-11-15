using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.RewardDetail;

namespace DB.Services
{
    public class RecommendRewardDetailService
    {
        #region Dto

        public static RecommendRewardDetailDto Find(int pk)
        {
            string sql = @"SELECT * FROM `recommend_reward_detail` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<RecommendRewardDetailDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardDetailService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RecommendRewardDetailDto> FindAll()
        {
            string sql = @"SELECT * FROM `recommend_reward_detail`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RecommendRewardDetailDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardDetailService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(RecommendRewardDetailDto model)
        {
            string sql = @"INSERT INTO `recommend_reward_detail` (
				`recommend_reward_fk`, `member_fk`, `parent`, `borrow_fee_fk`, `pk`, `borrow_date`, `currency`, `management_fee`, `generation`, `rate`, `reward`, `nickname`, `parent_nickname`)
				VALUES (@recommend_reward_fk, @member_fk, @parent, @borrow_fee_fk, @pk, @borrow_date, @currency, @management_fee, @generation, @rate, @reward, @nickname, @parent_nickname); ";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardDetailService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(RecommendRewardDetailDto model)
        {
            string sql = @"UPDATE `recommend_reward_detail` SET 
				`recommend_reward_fk` = @recommend_reward_fk,
				`member_fk` = @member_fk,
				`parent` = @parent,
				`borrow_fee_fk` = @borrow_fee_fk,
				`borrow_date` = @borrow_date,
				`currency` = @currency,
				`management_fee` = @management_fee,
				`generation` = @generation,
				`rate` = @rate,
				`reward` = @reward,
				`nickname` = @nickname,
				`parent_nickname` = @parent_nickname
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardDetailService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `recommend_reward_detail` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardDetailService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<RewardDetailList> FindRewardDetailList(int id)
        {
            string sql = @$"SELECT recommend_reward_detail.borrow_date, member.account, member.nickname, borrow_fee.sub_account, 
                (3-recommend_reward_detail.generation) AS generation, recommend_reward_detail.management_fee, recommend_reward_detail.rate, 
                recommend_reward_detail.reward, borrow_fee.type 
                FROM `recommend_reward_detail`
                INNER JOIN `member` ON member.pk = recommend_reward_detail.parent
                INNER JOIN borrow_fee ON borrow_fee.pk = recommend_reward_detail.borrow_fee_fk
                where recommend_reward_detail.recommend_reward_fk = {id}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RewardDetailList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRewardDetailService][FindRewardDetailList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
