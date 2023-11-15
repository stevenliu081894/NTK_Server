using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.WalletWithdraw;
using NTKServer.ViewModels.EndWalletWithdraw;

namespace DB.Services
{
    public class WalletWithdrawService
    {
        #region Dto

        public static WalletWithdrawDto Find(int pk)
        {
            string sql = @"SELECT * FROM `wallet_withdraw` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<WalletWithdrawDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletWithdrawDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet_withdraw`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletWithdrawDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(WalletWithdrawDto source)
        {
            string sql = @"INSERT INTO `wallet_withdraw` (
				`member_fk`, `member_bank_fk`, `order_no`, `wallet_amount`, `exchange`, `currency`, `money`, `fee`, `status`, `note`, `create_time`, `create_ip`, `verify_admin_pk`, `verify_time`, `reject_result`)
				VALUES (@member_fk, @member_bank_fk, @order_no, @wallet_amount, @exchange, @currency, @money, @fee, @status, @note, @create_time, @create_ip, @verify_admin_pk, @verify_time, @reject_result);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(WalletWithdrawDto model)
        {
            string sql = @"UPDATE `wallet_withdraw` SET 
				`member_fk` = @member_fk,
				`member_bank_fk` = @member_bank_fk,
				`order_no` = @order_no,
				`wallet_amount` = @wallet_amount,
				`exchange` = @exchange,
				`currency` = @currency,
				`money` = @money,
				`fee` = @fee,
				`status` = @status,
				`note` = @note,
				`create_time` = @create_time,
				`create_ip` = @create_ip,
				`verify_admin_pk` = @verify_admin_pk,
				`verify_time` = @verify_time,
				`reject_result` = @reject_result
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `wallet_withdraw` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel


        public static EndWalletWithdrawEditVm FindEndWalletWithdrawEditVm(int pk)
        {
            string sql = @$"SELECT wallet_withdraw.verify_time, wallet_withdraw.verify_admin_pk, wallet_withdraw.status, wallet_withdraw.reject_result, 
                wallet_withdraw.create_time, wallet_withdraw.order_no, wallet_withdraw.wallet_amount, wallet_withdraw.exchange, wallet_withdraw.currency, 
                wallet_withdraw.money, member_bank.card, member_bank.account as bank_account, member.account, member.nickname, member_bank.bank, member_bank.branch
                FROM `wallet_withdraw`
                INNER JOIN member_bank on member_bank.card_pk = wallet_withdraw.member_bank_fk
                INNER JOIN `member` on member.pk = wallet_withdraw.member_fk   
                where wallet_withdraw.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<EndWalletWithdrawEditVm>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][FindEndWalletWithdrawEditVm]" + ex.Message);
                return null;
            }
        }



	    public static List<EndWalletWithdrawList> FindEndWalletWithdrawList(string whereSql="")
        {
            string sql = @$"SELECT wallet_withdraw.verify_time, wallet_withdraw.verify_admin_pk, wallet_withdraw.reject_result, wallet_withdraw.create_time, 
                    wallet_withdraw.order_no, wallet_withdraw.wallet_amount, wallet_withdraw.exchange, wallet_withdraw.currency, wallet_withdraw.money, 
                    wallet_withdraw.pk, member_bank.bank, member_bank.branch, member_bank.card, member_bank.account as bank_account, member.account, 
                    member.nickname 
                    FROM `wallet_withdraw`
                    INNER JOIN member_bank on member_bank.card_pk = wallet_withdraw.member_bank_fk
                    INNER JOIN `member` on member.pk = wallet_withdraw.member_fk
                    {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<EndWalletWithdrawList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][FindEndWalletWithdrawList]" + ex.Message);
                return null;
            }
        }

	    public static List<WalletWithdrawList> FindWalletWithdrawList(string whereSql="")
        {
            string sql = @$"SELECT wallet_withdraw.create_time, wallet_withdraw.order_no, wallet_withdraw.wallet_amount, wallet_withdraw.exchange, 
                wallet_withdraw.currency, wallet_withdraw.money, wallet_withdraw.pk, member_bank.bank, member_bank.branch, member_bank.card, 
                member_bank.account AS bank_account, member.account, member.nickname 
                FROM `wallet_withdraw`
                INNER JOIN member_bank on member_bank.card_pk = wallet_withdraw.member_bank_fk
                INNER JOIN `member` on member.pk = wallet_withdraw.member_fk
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletWithdrawList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][FindWalletWithdrawList]" + ex.Message);
                return null;
            }
        }


        public static WalletWithdrawReview FindWalletWithdrawReview(int pk)
        {
            string sql = @$"SELECT wallet_withdraw.create_time, wallet_withdraw.order_no, wallet_withdraw.wallet_amount, wallet_withdraw.exchange, 
                wallet_withdraw.currency, wallet_withdraw.money, wallet_withdraw.member_bank_fk, wallet_withdraw.pk, wallet_withdraw.status, member_bank.currency, 
                member_bank.is_confirm, member_bank.bank, member_bank.branch, member_bank.card, member_bank.account AS bank_account, member.account, 
                member.nickname, member.id_auth, member.admin_user_fk, member.pk AS member_fk
                FROM `wallet_withdraw`
                INNER JOIN member_bank on member_bank.card_pk = wallet_withdraw.member_bank_fk
                INNER JOIN `member` on member.pk = wallet_withdraw.member_fk
                WHERE wallet_withdraw.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<WalletWithdrawReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletWithdrawService][FindWalletWithdrawReview]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
