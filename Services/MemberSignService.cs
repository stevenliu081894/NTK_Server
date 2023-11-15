using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class MemberSignService
    {
        #region Dto

        public static MemberSignDto Find(int pk)
        {
            string sql = @"SELECT * FROM `member_sign` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<MemberSignDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberSignService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MemberSignDto> FindAll()
        {
            string sql = @"SELECT * FROM `member_sign`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberSignDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberSignService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MemberSignDto source)
        {
            string sql = @"INSERT INTO `member_sign` (
				`member_fk`, `sign_time`, `continuity_day`, `total_day`, `coupon`, `currency`)
				VALUES (@member_fk, @sign_time, @continuity_day, @total_day, @coupon, @currency);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberSignService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(MemberSignDto model)
        {
            string sql = @"UPDATE `member_sign` SET 
				`member_fk` = @member_fk,
				`sign_time` = @sign_time,
				`continuity_day` = @continuity_day,
				`total_day` = @total_day,
				`coupon` = @coupon,
				`currency` = @currency
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberSignService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `member_sign` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberSignService][Remove]" + ex.Message);
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
