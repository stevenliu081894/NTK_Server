using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Member;
using NTKServer.ViewModels.Demo;
using NTKServer.ViewModels.ReviewMember;

namespace DB.Services
{
    public class MemberService
    {
        #region Dto

        public static MemberDto Find(int pk)
        {
            string sql = @"SELECT * FROM `member` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<MemberDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MemberDto> FindAll()
        {
            string sql = @"SELECT * FROM `member`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MemberDto source)
        {
            string sql = @"INSERT INTO `member` (
				`admin_user_fk`, `id`, `account`, `nickname`, `real_name`, `email`, `mobile_country`, `mobile`, `passwd`, `token`, `sub_account`, `paywd`, `id_card`, `id_card_type`, `id_auth`, `status`, `is_del`, `create_time`, `create_ip`, `last_login_time`, `last_login_ip`, `urgent_name`, `urgent_mobile`, `auth_time`, `auth_result`, `head_img`, `card_pic_front`, `card_pic_back`, `card_pic_hand`, `invitation_code`, `recommend`, `recommend_id`, `card_pic`, `level_id`, `remark`, `country`, `time_zone`, `lang`, `sms_status`, `email_status`)
				VALUES (@admin_user_fk, @id, @account, @nickname, @real_name, @email, @mobile_country, @mobile, @passwd, @token, @sub_account, @paywd, @id_card, @id_card_type, @id_auth, @status, @is_del, @create_time, @create_ip, @last_login_time, @last_login_ip, @urgent_name, @urgent_mobile, @auth_time, @auth_result, @head_img, @card_pic_front, @card_pic_back, @card_pic_hand, @invitation_code, @recommend, @recommend_id, @card_pic, @level_id, @remark, @country, @time_zone, @lang, @sms_status, @email_status);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(MemberDto model)
        {
            string sql = @"UPDATE `member` SET 
				`admin_user_fk` = @admin_user_fk,
				`id` = @id,
				`account` = @account,
				`nickname` = @nickname,
				`real_name` = @real_name,
				`email` = @email,
				`mobile_country` = @mobile_country,
				`mobile` = @mobile,
				`passwd` = @passwd,
				`token` = @token,
				`sub_account` = @sub_account,
				`paywd` = @paywd,
				`id_card` = @id_card,
				`id_card_type` = @id_card_type,
				`id_auth` = @id_auth,
				`status` = @status,
				`is_del` = @is_del,
				`create_time` = @create_time,
				`create_ip` = @create_ip,
				`last_login_time` = @last_login_time,
				`last_login_ip` = @last_login_ip,
				`urgent_name` = @urgent_name,
				`urgent_mobile` = @urgent_mobile,
				`auth_time` = @auth_time,
				`auth_result` = @auth_result,
				`head_img` = @head_img,
				`card_pic_front` = @card_pic_front,
				`card_pic_back` = @card_pic_back,
				`card_pic_hand` = @card_pic_hand,
				`invitation_code` = @invitation_code,
				`recommend` = @recommend,
				`recommend_id` = @recommend_id,
				`card_pic` = @card_pic,
				`level_id` = @level_id,
				`remark` = @remark,
				`country` = @country,
				`time_zone` = @time_zone,
				`lang` = @lang,
				`sms_status` = @sms_status,
				`email_status` = @email_status
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

		public static int UpdateSubAccount(int pk, string subAccount)
		{
			string sql = @"UPDATE `member` SET 
				`sub_account` = @subAccount
				 WHERE `pk` = @pk";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				return conn.Execute(sql, new { pk,subAccount });
			}
			catch (Exception ex)
			{
				LogLib.Log("[MemberService][UpdateFull]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}

		public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `member` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion

        #region ViewModel
	    public static List<MemberList> FindMemberList(string whereSql="")
        {
            string sql = @$"SELECT member.admin_user_fk, admin_user.nickname AS admin_user_name, member.pk, member.account, member.nickname, member.real_name, member.lang, member.recommend_id, member.invitation_code, member.last_login_time, member.last_login_ip, member.level_id, member.remark 
                        FROM `member` 
                        LEFT JOIN `admin_user` ON admin_user.pk=member.admin_user_fk
                        {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberService][FindMemberList]" + ex.Message);
                return null;
            }
        }


        public static List<ReviewMemberList> FindReviewMemberList(string whereSql="")
        {
            string sql = @$"SELECT pk, account, nickname, real_name, email, create_time, create_ip, last_login_time, last_login_ip, auth_time, auth_result, recommend_id, country, lang,id_auth FROM `member`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<ReviewMemberList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberService][FindReviewMemberList]" + ex.Message);
                return null;
            }
        }


        public static List<DemoSearchList> FindDemoSearch(string where = "")
        {
            string sql = $"SELECT pk, sn, sub_account, request_time FROM `trade_money_check` {where}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<DemoSearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeMoneyCheckService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static VerifyMemberVm GetVerifyMember(int pk)
        {
            string sql = @"SELECT * FROM `member` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<VerifyMemberVm>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberService][GetVerifyMember]" + ex.Message);
                return null;
            }
        }

		#endregion

		#region Other

		public static int UpdateStatus(int pk, int status)
		{
			string sql = @"UPDATE `member` SET 
				`id_auth` = @status
				 WHERE `pk` = @pk";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				return conn.Execute(sql, new { pk, status });
			}
			catch (Exception ex)
			{
				LogLib.Log("[MemberService][UpdateFull]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}

		public static int UpdateAuthTimeAndResult(int pk, DateTime auth_time, string auth_result)
		{
			string sql = @"UPDATE `member` SET 
				`auth_time` = @auth_time,
				`auth_result` = @auth_result
				 WHERE `pk` = @pk";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				return conn.Execute(sql, new { pk, auth_time, auth_result });
			}
			catch (Exception ex)
			{
				LogLib.Log("[MemberService][UpdateFull]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}
		public static int UpdateAdminUserFk(int pk, int admin_user_fk)
		{
			string sql = @"UPDATE `member` SET 
				`admin_user_fk` = @admin_user_fk
				 WHERE `pk` = @pk";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				return conn.Execute(sql, new { pk, admin_user_fk });
			}
			catch (Exception ex)
			{
				LogLib.Log("[MemberService][UpdateFull]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}
        
        public static int UpdateInvitationCode(int pk, string code)
		{
			string sql = @"UPDATE `member` SET 
				`invitation_code` = @code
				 WHERE `pk` = @pk";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				return conn.Execute(sql, new { pk, code });
			}
			catch (Exception ex)
			{
				LogLib.Log("[MemberService][UpdateFull]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}

		public static int UpdateDel(int pk, int status)
		{
			string sql = @"UPDATE `member` SET 
				`is_del` = @status
				 WHERE `pk` = @pk";

			try
			{
				using var conn = DapperMysql.GetWriteConntion();
				return conn.Execute(sql, new { pk, status });
			}
			catch (Exception ex)
			{
				LogLib.Log("[MemberService][UpdateFull]" + ex.Message);
				throw new AppException(1030, "write_db_exception");
			}
		}


		#endregion
	}
}
