using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.RecommendRegister;

namespace DB.Services
{
    public class RecommendRegisterService
    {
        #region Dto

        public static RecommendRegisterDto Find(int invitee_fk)
        {
            string sql = @"SELECT * FROM `recommend_register` WHERE `invitee_fk` = @invitee_fk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {invitee_fk});
                return conn.QueryFirstOrDefault<RecommendRegisterDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRegisterService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RecommendRegisterDto> FindAll()
        {
            string sql = @"SELECT * FROM `recommend_register`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RecommendRegisterDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRegisterService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(RecommendRegisterDto model)
        {
			string sql = @"INSERT INTO `recommend_register` (
				`member_fk`, `invitee_fk`, `nickname`, `register_date`, `first_borrow_date`)
				VALUES (@member_fk, @invitee_fk, @nickname, @register_date, @first_borrow_date); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRegisterService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(RecommendRegisterDto model)
        {
            string sql = @"UPDATE `recommend_register` SET 
				`nickname` = @nickname,
				`register_date` = @register_date,
				`first_borrow_date` = @first_borrow_date
				 WHERE `invitee_fk` = @invitee_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRegisterService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

		public static int UpdateFirstBorrowDate(RecommendRegisterDto model)
		{
			string sql = @"UPDATE `recommend_register` SET 
				`first_borrow_date` = @first_borrow_date
				 WHERE `invitee_fk` = @invitee_fk";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new { invitee_fk = model.invitee_fk, first_borrow_date = DateTime.UtcNow });
			}
			catch (Exception ex)
			{
				LogLib.Log("[RecommendRegisterService][UpdateFirstBorrowDate]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}

		public static int Remove(int invitee_fk)
        {
            string sql = @"DELETE FROM `recommend_register` WHERE `member_fk` = `invitee_fk` = @invitee_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { invitee_fk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRegisterService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

	    public static List<RecommendRegisterList> FindRecommendRegisterList(string whereSql="")
        {
            string sql = @$"SELECT * FROM
                ( SELECT recommend_register.invitee_fk, member.create_time AS create_date, member.admin_user_fk, member.account, 
                    member.nickname, member.real_name, member.id_auth, member.level_id, member.invitation_code, recommend_register.first_borrow_date, 
                    invite.account AS Inviteaccount, invite.nickname AS Invitenickname 
                    FROM `recommend_register`
                    INNER JOIN `member` ON member.pk = recommend_register.invitee_fk
                    INNER JOIN `member` invite ON invite.pk = recommend_register.member_fk
                    WHERE recommend_register.member_fk <> 0                
                UNION
                SELECT recommend_register.invitee_fk, member.create_time AS create_date, member.admin_user_fk, member.account, 
                    member.nickname, member.real_name, member.id_auth, member.level_id, member.invitation_code, recommend_register.first_borrow_date, 
                    '' AS Inviteaccount, '' AS Invitenickname 
                    FROM `recommend_register`
                    INNER JOIN `member` ON member.pk = recommend_register.invitee_fk
                    WHERE recommend_register.member_fk = 0 )
                AS t 
                {whereSql}
                ORDER BY t.create_date DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RecommendRegisterList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRegisterService][FindRecommendRegisterList]" + ex.Message);
                return null;
            }
        }

        public static RecommendRegisterList FindRecommendRegister(int beinvite)
        {
            string sql = @$"SELECT recommend_register.invitee_fk, member.create_time AS create_date, member.admin_user_fk, member.account, 
                    member.nickname, member.real_name, member.id_auth, member.level_id, member.invitation_code, recommend_register.first_borrow_date, 
                    invite.account AS Inviteaccount, invite.nickname AS Invitenickname 
                    FROM `recommend_register`
                    INNER JOIN `member` ON member.pk = recommend_register.invitee_fk
                    INNER JOIN `member` invite ON invite.pk = recommend_register.member_fk
                    WHERE recommend_register.invitee_fk = {beinvite}";                
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<RecommendRegisterList>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RecommendRegisterService][FindRecommendRegister]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
