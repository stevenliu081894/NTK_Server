using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Ipwhitelist;

namespace DB.Services
{
    public class AdminIpwhitelistService
    {
        #region Dto

        public static AdminIpwhitelistDto Find(string ip)
        {
            string sql = @"SELECT * FROM `admin_ipwhitelist` WHERE `ip` = @ip";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {ip});
                return conn.QueryFirstOrDefault<AdminIpwhitelistDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminIpwhitelistService][Find]" + ex.Message);
                return null;
            }
        }

        public static AdminIpwhitelistDto FindByIpAndAccount(string ip, string account)
        {
            string sql = @"SELECT * FROM `admin_ipwhitelist` WHERE `ip` = @ip AND account = @account";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {ip, account});
                return conn.QueryFirstOrDefault<AdminIpwhitelistDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminIpwhitelistService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminIpwhitelistDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_ipwhitelist`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminIpwhitelistDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminIpwhitelistService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(AdminIpwhitelistDto model)
        {
			string sql = @"INSERT INTO `admin_ipwhitelist` (
				`ip`, `remarks`, `account`, `status`, `create_time`, `update_time`)
				VALUES (@ip, @remarks, @account, @status, @create_time, @update_time); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminIpwhitelistService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(AdminIpwhitelistDto model)
        {
            string sql = @"UPDATE `admin_ipwhitelist` SET 
				`remarks` = @remarks,
				`account` = @account,
				`status` = @status,
				`create_time` = @create_time,
				`update_time` = @update_time
				 WHERE `ip` = @ip";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminIpwhitelistService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string ip)
        {
            string sql = @"DELETE FROM `admin_ipwhitelist` WHERE `ip` = @ip";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { ip });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminIpwhitelistService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static List<IpwhitelistList> FindIpwhitelistList(string whereSql="")
        {
            string sql = $"SELECT * FROM `admin_ipwhitelist`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<IpwhitelistList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminIpwhitelistService][FindIpwhitelistList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region ViewModel

        #endregion

        #region Other

        #endregion
    }
}
