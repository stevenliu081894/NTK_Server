using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsFilesService
    {
        #region Dto

        public static CmsFilesDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_files` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsFilesDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsFilesService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsFilesDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_files`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsFilesDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsFilesService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsFilesDto source)
        {
            string sql = @"INSERT INTO `cms_files` (
				`url`, `file_type`, `table`, `key`)
				VALUES (@url, @file_type, @table, @key);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsFilesService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int Update(CmsFilesDto model)
        {
            string sql = @"UPDATE `cms_files` SET 
				`url` = @url,
				`file_type` = @file_type,
				`table` = @table
				 WHERE `key` = @key";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsFilesService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateKey(int pk, int key)
        {
            string sql = @"UPDATE `cms_files` SET 
				`key` = @key
				 WHERE `pk` = @pk";

            var param = DapperMysql.GetParameters(new { pk, key });
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsFilesService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_files` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsFilesService][Remove]" + ex.Message);
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
