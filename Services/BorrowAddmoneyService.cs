using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using X.PagedList;
using NTKServer.ViewModels.BorrowAddMoney;
using Microsoft.AspNetCore.Connections;
using NTKServer.ViewModels.BorrowAddmoney;
using NTKServer.ViewModels.HisAddmoney;

namespace DB.Services
{
    public class BorrowAddmoneyService
    {
        #region Dto

        public static BorrowAddmoneyDto Find(int pk)
        {
            string sql = @"SELECT * FROM `borrow_addmoney` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<BorrowAddmoneyDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<BorrowAddmoneyDto> FindAll()
        {
            string sql = @"SELECT * FROM `borrow_addmoney`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowAddmoneyDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(BorrowAddmoneyDto source)
        {
            string sql = @"INSERT INTO `borrow_addmoney` (
				`sub_account`, `member_fk`, `currency`, `exchange`, `money`, `freeze`, `status`, `add_time`, `verify_time`, `target_uid`, `target_name`)
				VALUES (@sub_account, @member_fk, @currency, @exchange, @money, @freeze, @status, @add_time, @verify_time, @target_uid, @target_name);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(BorrowAddmoneyDto model)
        {
            string sql = @"UPDATE `borrow_addmoney` SET 
				`sub_account` = @sub_account,
				`member_fk` = @member_fk,
				`currency` = @currency,
				`exchange` = @exchange,
				`money` = @money,
				`freeze` = @freeze,
				`status` = @status,
				`add_time` = @add_time,
				`verify_time` = @verify_time,
				`target_uid` = @target_uid,
				`target_name` = @target_name
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `borrow_addmoney` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<HisAddmoneyList> FindHisAddmoneyList(string whereSql="")
        {
            string sql = @$"SELECT vw_trade_account.account, vw_trade_account.member_name, borrow_addmoney.sub_account, vw_trade_account.loan_type, 
                vw_trade_account.market, borrow_addmoney.add_time, borrow_addmoney.money, borrow_addmoney.pk, borrow_addmoney.status, 
                borrow_addmoney.verify_time, borrow_addmoney.target_name 
                FROM `borrow_addmoney`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_addmoney.sub_account
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<HisAddmoneyList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][FindHisAddmoneyList]" + ex.Message);
                return null;
            }
        }

        public static HisAddmoneyEditVm FindHisAddmoneyEditVm(int pk)
        {
            string sql = @$"SELECT vw_trade_account.account, vw_trade_account.member_name, borrow_addmoney.sub_account, vw_trade_account.loan_type, 
                vw_trade_account.market, borrow_addmoney.add_time, borrow_addmoney.money, borrow_addmoney.status, borrow_addmoney.verify_time, 
                borrow_addmoney.target_name 
                FROM `borrow_addmoney`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_addmoney.sub_account
                WHERE borrow_addmoney.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<HisAddmoneyEditVm>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][FindHisAddmoneyEditVm]" + ex.Message);
                return null;
            }
        }


	    public static List<BorrowAddmoneyList> FindBorrowAddmoneyList(string whereSql="")
        {
            string sql = @$"SELECT borrow_addmoney.add_time, borrow_addmoney.sub_account, borrow_addmoney.money, borrow_addmoney.currency, 
                vw_trade_account.status as trade_status, vw_trade_account.end_time, vw_trade_account.market, vw_trade_account.init_money, 
                vw_trade_account.balance, vw_trade_account.warningline, vw_trade_account.breakline, vw_trade_account.loan_type, 
                borrow_addmoney.member_fk, vw_trade_account.account, vw_trade_account.member_name, borrow_addmoney.pk 
                FROM `borrow_addmoney`
                INNER JOIN vw_trade_account ON vw_trade_account.sub_account = borrow_addmoney.sub_account
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowAddmoneyList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][FindBorrowAddmoneyList]" + ex.Message);
                return null;
            }
        }


        public static BorrowAddmoneyReview FindBorrowAddmoneyReview(int pk)
        {
            string sql = @$"SELECT borrow_addmoney.add_time, borrow_addmoney.sub_account, borrow_addmoney.money, borrow_addmoney.currency, 
                vw_trade_account.status as trade_status, vw_trade_account.end_time, vw_trade_account.market, vw_trade_account.init_money, 
                vw_trade_account.balance, vw_trade_account.warningline, vw_trade_account.breakline, vw_trade_account.loan_type, 
                borrow_addmoney.member_fk, vw_trade_account.account, vw_trade_account.member_name, borrow_addmoney.pk, borrow_addmoney.exchange, 
                borrow_addmoney.freeze, borrow_addmoney.status, borrow_addmoney.verify_time, borrow_addmoney.target_uid, 
                borrow_addmoney.target_name 
                FROM `borrow_addmoney`
                INNER JOIN vw_trade_account ON vw_trade_account.sub_account = borrow_addmoney.sub_account
                where borrow_addmoney.pk = {pk}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<BorrowAddmoneyReview>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][FindBorrowAddmoneyReview]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        public static List<AddMoneySearchList> GetAddMoney(string where)
        {
            string sql = $"SELECT b.pk as pk, b.member_fk AS member_fk, m.real_name AS member_real_name, b.sub_account AS sub_account, b.status AS status, v.currency AS currency, b.money AS money, v.balance AS balance, v.warningline AS warningline, v.breakline AS breakline, b.exchange AS exchange, b.freeze AS freeze, b.add_time AS add_time, b.verify_time AS verify_time, b.target_name AS target_name " +
                $"FROM borrow_addmoney AS b " +
                $"LEFT JOIN member AS m ON m.pk = b.member_fk " +
                $"LEFT JOIN vw_trade_account AS v ON v.sub_account = b.sub_account {where};";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AddMoneySearchList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][GetAddMoney]" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 更新審核狀態
        /// </summary>
        public static int UpdateStatus(int id, bool state)
        {
            const string sql = @"UPDATE `borrow_addmoney` SET status = @status, verify_time = @verify_time
WHERE pk=@pk;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, new
                {
                    pk = id,
                    status = state ? (int)VerifyStatusType.Successful : (int)VerifyStatusType.Fail,
                    verify_time = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowAddmoneyService][UpdateStatus]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion
    }
}
