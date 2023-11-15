using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class BorrowDetailService
    {
        #region Dto

        public static BorrowDetailDto Find(int pk)
        {
            string sql = @"SELECT * FROM `borrow_detail` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<BorrowDetailDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowDetailService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<BorrowDetailDto> FindAll()
        {
            string sql = @"SELECT * FROM `borrow_detail`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BorrowDetailDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowDetailService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(BorrowDetailDto source)
        {
            string sql = @"INSERT INTO `borrow_detail` (
				`borrow_fk`, `member_fk`, `status`, `interest`, `receive_interest`, `sort_order`, `total`, `deadline`, `repayment_time`)
				VALUES (@borrow_fk, @member_fk, @status, @interest, @receive_interest, @sort_order, @total, @deadline, @repayment_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowDetailService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(BorrowDetailDto model)
        {
            string sql = @"UPDATE `borrow_detail` SET 
				`borrow_fk` = @borrow_fk,
				`member_fk` = @member_fk,
				`status` = @status,
				`interest` = @interest,
				`receive_interest` = @receive_interest,
				`sort_order` = @sort_order,
				`total` = @total,
				`deadline` = @deadline,
				`repayment_time` = @repayment_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowDetailService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `borrow_detail` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[BorrowDetailService][Remove]" + ex.Message);
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
