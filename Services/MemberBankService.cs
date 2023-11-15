using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.MemberBank;

namespace DB.Services
{
    public class MemberBankService
    {
        #region Dto

        public static MemberBankDto Find(string card_pk)
        {
            string sql = @"SELECT * FROM `member_bank` WHERE `card_pk` = @card_pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {card_pk});
                return conn.QueryFirstOrDefault<MemberBankDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MemberBankDto> FindAll()
        {
            string sql = @"SELECT * FROM `member_bank`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberBankDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(MemberBankDto model)
        {
			string sql = @"INSERT INTO `member_bank` (
				`member_fk`, `card_pk`, `card_type`, `currency`, `country`, `bank`, `branch`, `card`, `account`, `cms_files_fk`, `is_confirm`, `is_delete`, `create_ip`, `create_time`)
				VALUES (@member_fk, @card_pk, @card_type, @currency, @country, @bank, @branch, @card, @account, @cms_files_fk, @is_confirm, @is_delete, @create_ip, @create_time); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(MemberBankDto model)
        {
            string sql = @"UPDATE `member_bank` SET 
				`member_fk` = @member_fk,
				`card_type` = @card_type,
				`currency` = @currency,
				`country` = @country,
				`bank` = @bank,
				`branch` = @branch,
				`card` = @card,
				`account` = @account,
				`cms_files_fk` = @cms_files_fk,
				`is_confirm` = @is_confirm,
				`is_delete` = @is_delete,
				`create_ip` = @create_ip,
				`create_time` = @create_time
				 WHERE `card_pk` = @card_pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateStatus(string card_pk, bool is_confirm)
        {
            string sql = @"UPDATE `member_bank` SET 
				`is_confirm` = @is_confirm
				 WHERE `card_pk` = @card_pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { card_pk, is_confirm });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string card_pk)
        {
            string sql = @"DELETE FROM `member_bank` WHERE `card_pk` = @card_pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { card_pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<MemberBankList> FindMemberBankList(int member)
        {
            string sql = @$"SELECT member_bank.card_pk, member_bank.card_type, member_bank.currency, member_bank.bank, member_bank.branch, 
                member_bank.card, member_bank.account as bank_account, member_bank.is_confirm, member.account, member.nickname, member.real_name
                FROM `member_bank`
                INNER JOIN member on member.pk = member_bank.member_fk 
                where member_bank.member_fk = {member}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberBankList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][FindMemberBankList]" + ex.Message);
                return null;
            }
        }


        public static MemberBankReview FindMemberBankReview(string card_pk)
        {
            string sql = @$"SELECT member_bank.member_fk, member_bank.card_pk, member_bank.card_type, member_bank.currency, member_bank.bank, 
                member_bank.branch, member_bank.card, member_bank.account as bank_account, member_bank.is_confirm, member_bank.create_ip, 
                member_bank.create_time, member.account, member.nickname, member.real_name, member.id_auth, cms_files.url as card_front
                FROM `member_bank`
                INNER JOIN member on member.pk = member_bank.member_fk 
                INNER JOIN cms_files on cms_files.pk = member_bank.cms_files_fk 
                WHERE member_bank.card_pk = '{card_pk}'";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<MemberBankReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][FindMemberBankReview]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other
        public static int UpdateIsConfirm(string card_pk, bool is_confirm)
        {
            string sql = @"UPDATE `member_bank` SET 
        `is_confirm` = @is_confirm
        WHERE `card_pk` = @card_pk";

            var parameters = new { is_confirm, card_pk };

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, parameters);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][UpdateIsConfirm]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateIsDelete(string card_pk, bool is_delete)
        {
            string sql = @"UPDATE `member_bank` SET 
        `is_delete` = @is_delete
        WHERE `card_pk` = @card_pk";

            var parameters = new { is_delete, card_pk };

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, parameters);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberBankService][UpdateIsDelete]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion
    }
}
