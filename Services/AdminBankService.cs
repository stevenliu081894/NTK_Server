using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.AdminBank;

namespace DB.Services
{
    public class AdminBankService
    {
        #region Dto

        public static AdminBankDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_bank` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminBankDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminBankService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminBankDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_bank`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminBankDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminBankService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminBankDto source)
        {
            string sql = @"INSERT INTO `admin_bank` (
				`type`, `country`, `currency`, `card`, `SWIFT`, `bank_name`, `open_bank`, `payee`, `notes`, `status`, `image`, `viplists`, `bankimgid`)
				VALUES (@type, @country, @currency, @card, @SWIFT, @bank_name, @open_bank, @payee, @notes, @status, @image, @viplists, @bankimgid);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminBankService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminBankDto model)
        {
            string sql = @"UPDATE `admin_bank` SET 
				`type` = @type,
				`country` = @country,
				`currency` = @currency,
				`card` = @card,
				`SWIFT` = @SWIFT,
				`bank_name` = @bank_name,
				`open_bank` = @open_bank,
				`payee` = @payee,
				`notes` = @notes,
				`status` = @status,
				`image` = @image,
				`viplists` = @viplists,
				`bankimgid` = @bankimgid
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminBankService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_bank` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminBankService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

	    public static List<AdminBankList> FindAdminBankList(string whereSql="")
        {
            string sql = @$"SELECT admin_bank.pk, admin_bank.type, admin_bank.currency, admin_bank.card, admin_bank.bank_name, admin_bank.open_bank, admin_bank.payee, admin_bank.notes, admin_bank.status, admin_bank.viplists FROM `admin_bank`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminBankList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminBankService][FindAdminBankList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
