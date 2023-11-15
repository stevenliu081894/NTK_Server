using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.MemberTask;

namespace DB.Services
{
    public class MemberTaskService
    {
        #region Dto

        public static MemberTaskDto Find(int pk)
        {
            string sql = @"SELECT * FROM `member_task` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<MemberTaskDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberTaskService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MemberTaskDto> FindAll()
        {
            string sql = @"SELECT * FROM `member_task`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberTaskDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberTaskService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MemberTaskDto source)
        {
            string sql = @"INSERT INTO `member_task` (
				`sub_type`, `currency`, `lang`, `coin`, `title`, `content`)
				VALUES (@sub_type, @currency, @lang, @coin, @title, @content);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberTaskService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(MemberTaskDto model)
        {
            string sql = @"UPDATE `member_task` SET 
				`sub_type` = @sub_type,
				`currency` = @currency,
				`lang` = @lang,
				`coin` = @coin,
				`title` = @title,
				`content` = @content
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberTaskService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `member_task` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberTaskService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<MemberTaskList> FindMemberTaskList(string whereSql="")
        {
            string sql = @$"SELECT member_task.pk, member_task.sub_type, member_task.currency, member_task.coin, member_task.lang, member_task.title, member_task.content FROM `member_task`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MemberTaskList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MemberTaskService][FindMemberTaskList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other
        //(sub_type,lang,currency)
        public static MemberTaskDto FindBySubType_Currency_Lang(int sub_type, string lang, string currency)
        {
            string sql = @"SELECT * FROM `member_task` 
WHERE `sub_type` = @sub_type AND `lang` = @lang AND `currency` = @currency";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { sub_type, lang, currency });
                return conn.QueryFirstOrDefault<MemberTaskDto>(sql, param);
            }
            catch (Exception ex)
            {
                throw new AppException(1040, "read_db_exception");
            }
        }
        #endregion
    }
}
