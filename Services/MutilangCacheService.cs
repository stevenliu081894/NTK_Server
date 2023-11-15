using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class MutilangCacheService
    {
        #region Dto

        public static MutilangCacheDto Find(string key, string lang)
        {
            string sql = @"SELECT * FROM `mutilang_cache` WHERE `key` = @key AND `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {key, lang});
                return conn.QueryFirstOrDefault<MutilangCacheDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangCacheService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MutilangCacheDto> FindAll()
        {
            string sql = @"SELECT * FROM `mutilang_cache`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MutilangCacheDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangCacheService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(MutilangCacheDto model)
        {
			string sql = @"INSERT INTO `mutilang_cache` (
				`key`, `lang`, `value`)
				VALUES (@key, @lang, @value); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangCacheService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(MutilangCacheDto model)
        {
            string sql = @"UPDATE `mutilang_cache` SET 
				`value` = @value
				 WHERE `key` = @key AND `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangCacheService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Update(MutilangCacheDto model)
        {
            string sql = @"UPDATE `mutilang_cache` SET 
				`value` = @value
				 WHERE `key` = @key AND `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangCacheService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string key, string lang)
        {
            string sql = @"DELETE FROM `mutilang_cache` WHERE `key` = @key AND `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { key, lang });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangCacheService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

        public static List<MutilangCacheDto> GetMultiLangCacheList(string where)
        {
            var sql = $"SELECT * FROM `mutilang_cache` {where}";
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MutilangCacheDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangCacheService][GetMultiLangCacheList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other
        public static string FindJson(string key, string lang)
        {
            string sql = @"SELECT value FROM `mutilang_cache` WHERE `key` = @key AND `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { key, lang });
                return conn.QueryFirstOrDefault<string>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangCacheService][Find]" + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
