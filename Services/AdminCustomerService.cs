using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class AdminCustomerService
    {
        #region Dto

        public static AdminCustomerDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_customer` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminCustomerDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminCustomerService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminCustomerDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_customer`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminCustomerDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminCustomerService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminCustomerDto source)
        {
            string sql = @"INSERT INTO `admin_customer` (
				`customer_name`, `enable`, `appkey`, `business_code`, `lang`, `app_url`, `contract_start_time`, `contract_end_time`, `exange`)
				VALUES (@customer_name, @enable, @appkey, @business_code, @lang, @app_url, @contract_start_time, @contract_end_time, @exange);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminCustomerService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminCustomerDto model)
        {
            string sql = @"UPDATE `admin_customer` SET 
				`customer_name` = @customer_name,
				`enable` = @enable,
				`appkey` = @appkey,
				`business_code` = @business_code,
				`lang` = @lang,
				`app_url` = @app_url,
				`contract_start_time` = @contract_start_time,
				`contract_end_time` = @contract_end_time,
				`exange` = @exange
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminCustomerService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_customer` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminCustomerService][Remove]" + ex.Message);
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
