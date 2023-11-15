using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class WalletCouponRecordService
    {
        #region Dto

        public static WalletCouponRecordDto Find(int pk)
        {
            string sql = @"SELECT * FROM `wallet_coupon_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<WalletCouponRecordDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletCouponRecordService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletCouponRecordDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet_coupon_record`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletCouponRecordDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletCouponRecordService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(WalletCouponRecordDto source)
        {
            string sql = @"INSERT INTO `wallet_coupon_record` (
				`member_fk`, `currency`, `affect`, `coupon_before`, `coupon_after`, `type`, `sub_type`, `info`, `create_time`)
				VALUES (@member_fk, @currency, @affect, @coupon_before, @coupon_after, @type, @sub_type, @info, @create_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletCouponRecordService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(WalletCouponRecordDto model)
        {
            string sql = @"UPDATE `wallet_coupon_record` SET 
				`member_fk` = @member_fk,
				`currency` = @currency,
				`affect` = @affect,
				`coupon_before` = @coupon_before,
				`coupon_after` = @coupon_after,
				`type` = @type,
				`sub_type` = @sub_type,
				`info` = @info,
				`create_time` = @create_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletCouponRecordService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `wallet_coupon_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletCouponRecordService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

        #endregion

        #region Other
        public static WalletCouponRecordDto FindByMemberFK_Type_SubType(int member_fk, int type, int sub_type)
        {
            string sql = @"SELECT * FROM `wallet_coupon_record` 
WHERE `member_fk` = @member_fk AND `type` = @type AND `sub_type` = @sub_type";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<WalletCouponRecordDto>(sql, new { member_fk, type, sub_type });
            }
            catch (Exception)
            {
                throw new AppException(1040, "read_db_exception");
            }
        }
        #endregion
    }
}
