using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.WalletPayment;

namespace DB.Services
{
    public class WalletPaymentService
    {
        #region Dto

        public static WalletPaymentDto Find(int pk)
        {
            string sql = @"SELECT * FROM `wallet_payment` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<WalletPaymentDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletPaymentService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletPaymentDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet_payment`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletPaymentDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletPaymentService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(WalletPaymentDto source)
        {
            string sql = @"INSERT INTO `wallet_payment` (
				`pay_name`, `pay_code`, `pay_type`, `pay_url`, `pay_account`, `pay_tokenkey`, `pay_Notice_url`, `pay_Return_url`, `pay_sort`, `status`, `create_time`, `notes`, `viplists`)
				VALUES (@pay_name, @pay_code, @pay_type, @pay_url, @pay_account, @pay_tokenkey, @pay_Notice_url, @pay_Return_url, @pay_sort, @status, @create_time, @notes, @viplists);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletPaymentService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(WalletPaymentDto model)
        {
            string sql = @"UPDATE `wallet_payment` SET 
				`pay_name` = @pay_name,
				`pay_code` = @pay_code,
				`pay_type` = @pay_type,
				`pay_url` = @pay_url,
				`pay_account` = @pay_account,
				`pay_tokenkey` = @pay_tokenkey,
				`pay_Notice_url` = @pay_Notice_url,
				`pay_Return_url` = @pay_Return_url,
				`pay_sort` = @pay_sort,
				`status` = @status,
				`create_time` = @create_time,
				`notes` = @notes,
				`viplists` = @viplists
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletPaymentService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `wallet_payment` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletPaymentService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<WalletPaymentList> FindWalletPaymentList(string whereSql="")
        {
            string sql = @$"SELECT wallet_payment.pk, wallet_payment.status, wallet_payment.pay_name, wallet_payment.pay_code, wallet_payment.viplists FROM `wallet_payment`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletPaymentList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletPaymentService][FindWalletPaymentList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
