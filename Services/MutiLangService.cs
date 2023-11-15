using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.MultiLang;

namespace DB.Services
{
    public class MutilangService
    {
        #region Dto

        public static MutilangDto Find(string key)
        {
            string sql = @"SELECT * FROM `mutilang` WHERE `key` = @key";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {key});
                return conn.QueryFirstOrDefault<MutilangDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MutilangDto> FindAll()
        {
            string sql = @"SELECT * FROM `mutilang`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MutilangDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(MutilangDto model)
        {
			string sql = @"INSERT INTO `mutilang` (
				`key`, `application`, `module`, `description`, `path`, `template`)
				VALUES (@key, @application, @module, @description, @path, @template); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(MutilangDto model)
        {
            string sql = @"UPDATE `mutilang` SET 
				`application` = @application,
				`module` = @module,
				`description` = @description,
				`path` = @path,
				`template` = @template
				 WHERE `key` = @key";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string key)
        {
            string sql = @"DELETE FROM `mutilang` WHERE `key` = @key";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { key });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        #endregion

        #region ViewModel
        internal static List<MultilangList> FindMutilangList(string whereSql = "")
        {
            string sql = @$"SELECT `key`, description, application FROM `mutilang` {whereSql}";
            
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MultilangList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangService][FindMutilangList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other
        internal static string FindJson(string key)
        {
            string sql = @"SELECT template FROM `mutilang` WHERE `key` = @key";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { key });
                return conn.QueryFirstOrDefault<string>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangService][FindJson]" + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
