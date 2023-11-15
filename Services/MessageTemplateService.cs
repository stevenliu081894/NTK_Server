using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.MessageTemplate;

namespace DB.Services
{
    public class MessageTemplateService
    {
        #region Dto

        public static MessageTemplateDto Find(int pk)
        {
            string sql = @"SELECT * FROM `message_template` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<MessageTemplateDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageTemplateService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MessageTemplateDto> FindAll()
        {
            string sql = @"SELECT * FROM `message_template`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MessageTemplateDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageTemplateService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MessageTemplateDto source)
        {
            string sql = @"INSERT INTO `message_template` (
				`temp_id`, `lang`, `status`, `name`, `type`, `receiver`, `title`, `title_param`, `param`, `template`, `remark`)
				VALUES (@temp_id, @lang, @status, @name, @type, @receiver, @title, @title_param, @param, @template, @remark);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageTemplateService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(MessageTemplateDto model)
        {
            string sql = @"UPDATE `message_template` SET 
				`temp_id` = @temp_id,
				`lang` = @lang,
				`status` = @status,
				`name` = @name,
				`type` = @type,
				`receiver` = @receiver,
				`title` = @title,
				`title_param` = @title_param,
				`param` = @param,
				`template` = @template,
				`remark` = @remark				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageTemplateService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `message_template` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageTemplateService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static MessageTemplateDto FindByTemplateId(int temp_id, string lang)
        {
            string strSql = @"SELECT * FROM message_template WHERE temp_id = @temp_id AND lang = @lang ";
            try
            {
                using (var conn = DapperMysql.GetReadConnection())
                {
                    var result = conn.QuerySingleOrDefault<MessageTemplateDto>(strSql, new { temp_id, lang });
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogLib.Log(ex.Message);
                throw new AppException(1040, "read_db_exception");
            }
        }
		
        #endregion

        #region ViewModel
	    public static List<MessageTemplateList> FindMessageTemplateList(string whereSql="")
        {
            string sql = @$"SELECT pk, message_template.temp_id, message_template.lang, message_template.status, message_template.name, message_template.type, message_template.title, message_template.title_param, message_template.param, message_template.template, message_template.remark FROM `message_template`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MessageTemplateList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageTemplateService][FindMessageTemplateList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
