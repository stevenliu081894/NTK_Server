using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class MemberInfoService
    {
        #region Dto

        public static MemberInfoDto Find(int member_fk)
        {
            string sql = @"SELECT * FROM `member_info` WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {member_fk});
                return conn.QueryFirstOrDefault<MemberInfoDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberInfoService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MemberInfoDto> FindAll()
        {
            string sql = @"SELECT * FROM `member_info`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberInfoDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberInfoService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(MemberInfoDto model)
        {
			string sql = @"INSERT INTO `member_info` (
				`member_fk`, `fristtime_save_money`, `lasttime_save_money`)
				VALUES (@member_fk, @fristtime_save_money, @lasttime_save_money); ";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberInfoService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(MemberInfoDto model)
        {
            string sql = @"UPDATE `member_info` SET 
				`fristtime_save_money` = @fristtime_save_money,
				`lasttime_save_money` = @lasttime_save_money
				 WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberInfoService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int member_fk)
        {
            string sql = @"DELETE FROM `member_info` WHERE `member_fk` = @member_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { member_fk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberInfoService][Remove]" + ex.Message);
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
