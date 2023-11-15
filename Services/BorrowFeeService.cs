using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.BorrowFee;

namespace DB.Services
{
    public class BorrowFeeService
    {
        #region Dto

        public static BorrowFeeDto Find(int pk)
        {
            string sql = @"SELECT * FROM `borrow_fee` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<BorrowFeeDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowFeeService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<BorrowFeeDto> FindAll()
        {
            string sql = @"SELECT * FROM `borrow_fee`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowFeeDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowFeeService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(BorrowFeeDto source)
        {
            string sql = @"INSERT INTO `borrow_fee` (
				`member_fk`, `sub_account`, `borrow_fk`, `type`, `borrow_fee`, `use_coupon`, `fee_received`, `borrow_duration`, `create_time`)
				VALUES (@member_fk, @sub_account, @borrow_fk, @type, @borrow_fee, @use_coupon, @fee_received, @borrow_duration, @create_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowFeeService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(BorrowFeeDto model)
        {
            string sql = @"UPDATE `borrow_fee` SET 
				`member_fk` = @member_fk,
				`sub_account` = @sub_account,
				`borrow_fk` = @borrow_fk,
				`type` = @type,
				`borrow_fee` = @borrow_fee,
				`use_coupon` = @use_coupon,
				`fee_received` = @fee_received,
				`borrow_duration` = @borrow_duration,
				`create_time` = @create_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowFeeService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `borrow_fee` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowFeeService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Insert(BorrowFeeDto model)
        {
            const string sql = "INSERT INTO borrow_fee (sub_account, member_fk, borrow_fk, type, borrow_fee, use_coupon, fee_received, borrow_duration, create_time) " +
                               "VALUES(@sub_account, @member_fk, @borrow_fk, @type, @borrow_fee, @use_coupon, @fee_received, @borrow_duration, @create_time);";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowFeeService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion

        #region ViewModel
	    public static List<BorrowFeeList> FindBorrowFeeList(string whereSql="")
        {
            string sql = @$"SELECT borrow_fee.create_time, borrow_fee.type, borrow_fee.borrow_fee, borrow_fee.sub_account, vw_trade_account.loan_type, 
                vw_trade_account.currency, borrow_fee.borrow_duration, vw_trade_account.account, vw_trade_account.member_name 
                FROM `borrow_fee`
                INNER JOIN vw_trade_account on vw_trade_account.sub_account = borrow_fee.sub_account
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowFeeList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowFeeService][FindBorrowFeeList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
