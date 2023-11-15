using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class RichboxRecordService
    {
        #region Dto

        public static RichboxRecordDto Find(int pk)
        {
            string sql = @"SELECT * FROM `richbox_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<RichboxRecordDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxRecordService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RichboxRecordDto> FindAll()
        {
            string sql = @"SELECT * FROM `richbox_record`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RichboxRecordDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxRecordService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(RichboxRecordDto source)
        {
            string sql = @"INSERT INTO `richbox_record` (
				`member_fk`, `affect`, `account`, `type`, `info`, `create_time`)
				VALUES (@member_fk, @affect, @account, @type, @info, @create_time);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxRecordService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(RichboxRecordDto model)
        {
            string sql = @"UPDATE `richbox_record` SET 
				`member_fk` = @member_fk,
				`affect` = @affect,
				`account` = @account,
				`type` = @type,
				`info` = @info,
				`create_time` = @create_time
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxRecordService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `richbox_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxRecordService][Remove]" + ex.Message);
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
