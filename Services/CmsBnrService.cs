using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsBnrService
    {
        #region Dto

        public static CmsBnrDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_bnr` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsBnrDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBnrService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsBnrDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_bnr`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsBnrDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBnrService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(CmsBnrDto model)
        {
			string sql = @"INSERT INTO `cms_bnr` (
				`pk`, `bnrid`, `msg`)
				VALUES (@pk, @bnrid, @msg); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBnrService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(CmsBnrDto model)
        {
            string sql = @"UPDATE `cms_bnr` SET 
				`bnrid` = @bnrid,
				`msg` = @msg
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBnrService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_bnr` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBnrService][Remove]" + ex.Message);
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
