using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsAdvertTypeService
    {
        #region Dto

        public static CmsAdvertTypeDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_advert_type` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsAdvertTypeDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertTypeService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsAdvertTypeDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_advert_type`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsAdvertTypeDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertTypeService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsAdvertTypeDto source)
        {
            string sql = @"INSERT INTO `cms_advert_type` (
				`name`, `status`)
				VALUES (@name, @status);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertTypeService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsAdvertTypeDto model)
        {
            string sql = @"UPDATE `cms_advert_type` SET 
				`name` = @name,
				`status` = @status
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertTypeService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_advert_type` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertTypeService][Remove]" + ex.Message);
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
