using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class WalletTransferService
    {
        #region Dto

        public static WalletTransferDto Find(int pk)
        {
            string sql = @"SELECT * FROM `wallet_transfer` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<WalletTransferDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTransferService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletTransferDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet_transfer`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletTransferDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTransferService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(WalletTransferDto source)
        {
            string sql = @"INSERT INTO `wallet_transfer` (
				`admin_user_fk`, `member_fk`, `order_no`, `money`, `create_time`, `create_ip`, `info`, `currency`)
				VALUES (@admin_user_fk, @member_fk, @order_no, @money, @create_time, @create_ip, @info, @currency);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTransferService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(WalletTransferDto model)
        {
            string sql = @"UPDATE `wallet_transfer` SET 
				`admin_user_fk` = @admin_user_fk,
				`member_fk` = @member_fk,
				`order_no` = @order_no,
				`money` = @money,
				`create_time` = @create_time,
				`create_ip` = @create_ip,
				`info` = @info,
				`currency` = @currency
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTransferService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `wallet_transfer` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTransferService][Remove]" + ex.Message);
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
