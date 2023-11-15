using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.WalletRecord;
using NTKServer.ViewModels.StatWalletRecordDetail;
using NTKServer.ViewModels.StatBalance;

namespace DB.Services
{
    public class WalletRecordService
    {
        #region Dto

        public static WalletRecordDto Find(int pk)
        {
            string sql = @"SELECT * FROM `wallet_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<WalletRecordDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRecordService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletRecordDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet_record`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletRecordDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRecordService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(WalletRecordDto source)
        {
            string sql = @"INSERT INTO `wallet_record` (
				`member_fk`, `type`, `subtype`, `currency`, `affect`, `freeze`, `balance`, `coupon`, `param`, `templat_id`, `info`, `create_time`, `create_ip`)
				VALUES (@member_fk, @type, @subtype, @currency, @affect, @freeze, @balance, @coupon, @param, @templat_id, @info, @create_time, @create_ip);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRecordService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(WalletRecordDto model)
        {
            string sql = @"UPDATE `wallet_record` SET 
				`member_fk` = @member_fk,
				`type` = @type,
				`subtype` = @subtype,
				`currency` = @currency,
				`affect` = @affect,
				`freeze` = @freeze,
				`balance` = @balance,
				`coupon` = @coupon,
				`param` = @param,
				`templat_id` = @templat_id,
				`info` = @info,
				`create_time` = @create_time,
				`create_ip` = @create_ip
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRecordService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `wallet_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRecordService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion

        #region ViewModel
	    public static List<StatWalletRecordDetailList> FindStatWalletRecordDetailList(string whereSql="")
        {
            string sql = @$"SELECT wallet_record.pk,  admin_user.account AS admin_account, member.admin_user_fk, member.account, member.nickname, wallet_record.type, 
                wallet_record.affect, wallet_record.create_time 
                ,wallet_template.name AS template_name
                FROM `wallet_record`
                INNER JOIN `member` ON wallet_record.member_fk = member.pk 
                INNER JOIN `sys_market` ON wallet_record.currency=sys_market.currency
                INNER JOIN `wallet_template` ON wallet_record.type = wallet_template.temp_id 
                LEFT JOIN `admin_user` ON `admin_user`.pk = `member`.admin_user_fk
                {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StatWalletRecordDetailList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRecordService][FindStatWalletRecordDetailList]" + ex.Message);
                return null;
            }
        }

	    public static List<WalletRecordList> FindWalletRecordList(int id)
        {
            string sql = @$"SELECT wallet_record.pk, wallet_record.type, wallet_record.currency, wallet_record.affect, wallet_record.freeze, 
                wallet_record.balance, wallet_record.coupon, wallet_record.info, wallet_record.create_time, wallet_record.create_ip 
               FROM `wallet_record` WHERE member_fk = {id} ";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletRecordList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRecordService][FindWalletRecordList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other
        public static List<StatBalanceList> FindStatBalanceList(string whereSql = "")
        {
            string sql = @$"SELECT * FROM
                (
                SELECT member.account, member.nickname, wallet_record.create_time, wallet_template.name, affect AS recharge, 
                0 AS withdraw, wallet_record.type
                FROM `wallet_record` 
                LEFT JOIN `member` ON `member`.pk = wallet_record.member_fk
                LEFT JOIN `wallet_template` ON wallet_template.temp_id = wallet_record.type 
                WHERE wallet_record.type = 1 AND wallet_template.lang = 'CN'

                UNION

                SELECT member.account, member.nickname, wallet_record.create_time, wallet_template.name, 0 AS recharge, 
                affect AS withdraw, wallet_record.type
                FROM `wallet_record` 
                LEFT JOIN `member` ON `member`.pk = wallet_record.member_fk
                LEFT JOIN `wallet_template` ON wallet_template.temp_id = wallet_record.type 
                WHERE wallet_record.type = 12 AND wallet_template.lang = 'CN'
                ) AS t
                {whereSql}
                ORDER BY t.create_time";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<StatBalanceList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletRecordService][FindStatBalanceList]" + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
