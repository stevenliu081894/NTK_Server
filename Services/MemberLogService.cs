using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class MemberLogService
    {
        #region Dto

        public static MemberLogDto Find(int pk)
        {
            string sql = @"SELECT * FROM `member_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<MemberLogDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLogService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MemberLogDto> FindAll()
        {
            string sql = @"SELECT * FROM `member_log`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberLogDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLogService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MemberLogDto source)
        {
            string sql = @"INSERT INTO `member_log` (
				`member_fk`, `addtime`, `ip`, `urlpath`, `info`, `urltype`, `udevice`, `ipinfo`)
				VALUES (@member_fk, @addtime, @ip, @urlpath, @info, @urltype, @udevice, @ipinfo);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLogService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(MemberLogDto model)
        {
            string sql = @"UPDATE `member_log` SET 
				`member_fk` = @member_fk,
				`addtime` = @addtime,
				`ip` = @ip,
				`urlpath` = @urlpath,
				`info` = @info,
				`urltype` = @urltype,
				`udevice` = @udevice,
				`ipinfo` = @ipinfo
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLogService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `member_log` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberLogService][Remove]" + ex.Message);
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
