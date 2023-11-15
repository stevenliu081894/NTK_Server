using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class RichboxCoreService
    {
        #region Dto

        public static RichboxCoreDto Find(int pk)
        {
            string sql = @"SELECT * FROM `richbox_core` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<RichboxCoreDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxCoreService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RichboxCoreDto> FindAll()
        {
            string sql = @"SELECT * FROM `richbox_core`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RichboxCoreDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxCoreService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(RichboxCoreDto source)
        {
            string sql = @"INSERT INTO `richbox_core` (
				`member_fk`, `create_time`, `invest`, `money`, `days`, `pay`)
				VALUES (@member_fk, @create_time, @invest, @money, @days, @pay);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxCoreService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(RichboxCoreDto model)
        {
            string sql = @"UPDATE `richbox_core` SET 
				`member_fk` = @member_fk,
				`create_time` = @create_time,
				`invest` = @invest,
				`money` = @money,
				`days` = @days,
				`pay` = @pay
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxCoreService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `richbox_core` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxCoreService][Remove]" + ex.Message);
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
