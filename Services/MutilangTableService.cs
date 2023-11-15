using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class MutilangTableService
    {
        #region Dto

        public static MutilangTableDto Find(int pk)
        {
            string sql = @"SELECT * FROM `mutilang_table` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<MutilangTableDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangTableService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MutilangTableDto> FindAll()
        {
            string sql = @"SELECT * FROM `mutilang_table`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MutilangTableDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangTableService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MutilangTableDto source)
        {
            string sql = @"INSERT INTO `mutilang_table` (
				`dbtable`, `field`, `key`, `lang`, `value`)
				VALUES (@dbtable, @field, @key, @lang, @value);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangTableService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(MutilangTableDto model)
        {
            string sql = @"UPDATE `mutilang_table` SET 
				`dbtable` = @dbtable,
				`field` = @field,
				`key` = @key,
				`lang` = @lang,
				`value` = @value
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangTableService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `mutilang_table` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangTableService][Remove]" + ex.Message);
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
