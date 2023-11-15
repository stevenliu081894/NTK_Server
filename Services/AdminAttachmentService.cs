using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class AdminAttachmentService
    {
        #region Dto

        public static AdminAttachmentDto Find(int pk)
        {
            string sql = @"SELECT * FROM `admin_attachment` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<AdminAttachmentDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminAttachmentService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<AdminAttachmentDto> FindAll()
        {
            string sql = @"SELECT * FROM `admin_attachment`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<AdminAttachmentDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminAttachmentService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(AdminAttachmentDto source)
        {
            string sql = @"INSERT INTO `admin_attachment` (
				`member_fk`, `name`, `module`, `path`, `thumb`, `url`, `mime`, `ext`, `size`, `md5`, `sha1`, `driver`, `download`, `sort`, `status`)
				VALUES (@member_fk, @name, @module, @path, @thumb, @url, @mime, @ext, @size, @md5, @sha1, @driver, @download, @sort, @status);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminAttachmentService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(AdminAttachmentDto model)
        {
            string sql = @"UPDATE `admin_attachment` SET 
				`member_fk` = @member_fk,
				`name` = @name,
				`module` = @module,
				`path` = @path,
				`thumb` = @thumb,
				`url` = @url,
				`mime` = @mime,
				`ext` = @ext,
				`size` = @size,
				`md5` = @md5,
				`sha1` = @sha1,
				`driver` = @driver,
				`download` = @download,
				`sort` = @sort,
				`status` = @status
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminAttachmentService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `admin_attachment` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[AdminAttachmentService][Remove]" + ex.Message);
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
