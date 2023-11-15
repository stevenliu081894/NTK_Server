using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class WalletFreezeService
    {
        #region Dto

        public static WalletFreezeDto Find(int pk)
        {
            string sql = @"SELECT * FROM `wallet_freeze` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<WalletFreezeDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletFreezeService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletFreezeDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet_freeze`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletFreezeDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletFreezeService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(WalletFreezeDto source)
        {
            string sql = @"INSERT INTO `wallet_freeze` (
				`member_fk`, `sn`, `freeze`, `subtype`, `create_time`)
				VALUES (@member_fk, @sn, @freeze, @subtype, @create_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletFreezeService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(WalletFreezeDto model)
        {
            string sql = @"UPDATE `wallet_freeze` SET 
				`member_fk` = @member_fk,
				`sn` = @sn,
				`freeze` = @freeze,
				`subtype` = @subtype,
				`create_time` = @create_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletFreezeService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `wallet_freeze` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletFreezeService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static void Insert(WalletFreezeDto walletfreezeDto)
        {
            string sql = @"INSERT INTO `wallet_freeze`  (`member_fk`, `sn`, `freeze`, `subtype`, `create_time`) 
                           VALUES (@member_fk, @sn, @freeze, @subtype, @create_time)";

            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    conn.Execute(sql, walletfreezeDto);
                }
            }
            catch (Exception e)
            {
                throw new AppException(1040, "write_db_exception");
            }
        }

        public void Delete(int id)
        {
            string sql = @"DELETE FROM wallet_freeze WHERE pk = @id";

            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    var param = DapperMysql.GetParameters(new { id = id });
                    conn.Execute(sql, param);
                }
            }
            catch (Exception e)
            {
                LogLib.Log("[WalletFreezeService][Delete]" + e.Message);
                throw new AppException(1030, "[1030] M线路忙碌,稍後再试");
            }
        }

        //用序號找資料
        public WalletFreezeDto FindWalletFreezeBySn(string sn)
        {
            string sql = @"SELECT * FROM wallet_freeze WHERE sn = @sn ";

            try
            {
                using (var conn = DapperMysql.GetReadConnection())
                {
                    var param = DapperMysql.GetParameters(new { sn });
                    return conn.QuerySingleOrDefault<WalletFreezeDto>(sql, param);
                }
            }
            catch (Exception e)
            {
                LogLib.Log("[WalletFreezeService][FindWalletFreezeBySn]" + e.Message);
                throw new AppException(1040, "[1040] S线路忙碌,稍後再试");
            }
        }

        //用合約編號SN 刪除資料
        public static void DeleteBySN(int member_fk, string sn)
        {
            string sql = @"DELETE FROM wallet_freeze WHERE member_fk = @member_fk and sn= @sn";

            try
            {
                using (var conn = DapperMysql.GetWriteConntion())
                {
                    var param = DapperMysql.GetParameters(new { member_fk = member_fk, sn = sn });
                    conn.Execute(sql, param);
                }
            }
            catch (Exception e)
            {
                LogLib.Log("[WalletFreezeService][Delete]" + e.Message);
                throw new AppException(1030, "[1030] M线路忙碌,稍後再试");
            }
        }
        #endregion

        #region ViewModel

        #endregion

        #region Other

        #endregion
    }
}
