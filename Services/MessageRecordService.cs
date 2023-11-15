using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.MessageRecord;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DB.Services
{
    public class MessageRecordService
    {
        #region Dto

        public static MessageRecordDto Find(int pk)
        {
            string sql = @"SELECT * FROM `message_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<MessageRecordDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageRecordService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MessageRecordDto> FindAll()
        {
            string sql = @"SELECT * FROM `message_record`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MessageRecordDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageRecordService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(MessageRecordDto source)
        {
            string sql = @"INSERT INTO `message_record` (
				`isbatch`, `receiver_table`, `receiver_fk`, `sender_table`, `sender_fk`, `title`, `info`, `read_status`, `type`, `send_status`, `send_type`, `create_time`, `read_time`, `sent_time`, `classify`)
				VALUES (@isbatch, @receiver_table, @receiver_fk, @sender_table, @sender_fk, @title, @info, @read_status, @type, @send_status, @send_type, @create_time, @read_time, @sent_time, @classify);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageRecordService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(MessageRecordDto model)
        {
            string sql = @"UPDATE `message_record` SET 
				`isbatch` = @isbatch,
				`receiver_table` = @receiver_table,
				`receiver_fk` = @receiver_fk,
				`sender_table` = @sender_table,
				`sender_fk` = @sender_fk,
				`title` = @title,
				`info` = @info,
				`read_status` = @read_status,
				`type` = @type,
				`send_status` = @send_status,
				`send_type` = @send_type,
				`create_time` = @create_time,
				`read_time` = @read_time,
				`sent_time` = @sent_time,
				`classify` = @classify
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageRecordService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `message_record` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageRecordService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<MessageRecordList> FindMessageRecordList(string whereSql="")
        {
            string sql = @$"SELECT message_record.pk, member.nickname, message_record.title, member.account, member.mobile_country, member.mobile, message_record.create_time sent_time, message_record.read_time 
                            FROM `message_record`
                            INNER JOIN member ON member.pk = message_record.sender_fk
                            {whereSql}
                            ORDER BY message_record.pk DESC";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MessageRecordList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageRecordService][FindMessageRecordList]" + ex.Message);
                return null;
            }
        }

        public static MessageRecordEditVm FindMessageRecordEditVm(int pk)
        {
            string sql = @$"SELECT message_record.sender_fk as receiver_fk, message_record.title, member.account, member.nickname, message_record.info, message_record.read_time as read_time FROM `message_record`
                            INNER JOIN member ON member.pk = message_record.sender_fk
                            WHERE message_record.pk = @pk";
            string sql_read = @"UPDATE `message_record` SET 
                                `read_status` = 1,
                                `read_time` = @read_time
                                WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                var record = conn.QueryFirstOrDefault<MessageRecordEditVm>(sql, param);
                if (record.read_time == null) {
                    DateTime read_time = DateTime.UtcNow;
                    param = DapperMysql.GetParameters(new {pk, read_time});
                    conn.Execute(sql_read, param);
                    record.read_time = read_time;
                }
                return record;
            }
            catch (Exception ex)
            {
                LogLib.Log("[MessageRecordService][FindMessageRecordEditVm]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
