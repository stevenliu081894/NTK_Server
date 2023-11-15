using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.SysCountry;

namespace DB.Services
{
    public class SysCountryService
    {
        #region Dto

        public static SysCountryDto Find(string pk)
        {
            string sql = @"SELECT * FROM `sys_country` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<SysCountryDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysCountryService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<SysCountryDto> FindAll()
        {
            string sql = @"SELECT * FROM `sys_country`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<SysCountryDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysCountryService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(SysCountryDto model)
        {
			string sql = @"INSERT INTO `sys_country` (
				`pk`, `label`, `enable`, `lang`, `currency`, `flag`, `code`)
				VALUES (@pk, @label, @enable, @lang, @currency, @flag, @code); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysCountryService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(SysCountryDto model)
        {
            string sql = @"UPDATE `sys_country` SET 
				`label` = @label,
				`enable` = @enable,
				`lang` = @lang,
				`currency` = @currency,
				`flag` = @flag,
				`code` = @code
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysCountryService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string pk)
        {
            string sql = @"DELETE FROM `sys_country` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysCountryService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
        public static List<SysCountryList> FindSysCountryList(string whereSql = "")
        {
            string sql = @$"SELECT sys_country.pk, sys_country.label, sys_country.enable, sys_country.lang, sys_country.currency, sys_country.flag, sys_country.code FROM `sys_country`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<SysCountryList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[SysCountryService][FindSysCountryList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
