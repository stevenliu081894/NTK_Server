using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class CmsAdvertService
    {
        #region Dto

        public static CmsAdvertDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_advert` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsAdvertDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsAdvertDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_advert`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsAdvertDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsAdvertDto source)
        {
            string sql = @"INSERT INTO `cms_advert` (
				`typeid`, `tagname`, `ad_type`, `timeset`, `start_time`, `end_time`, `name`, `content`, `expcontent`, `status`, `bnr_id`, `marq_id`, `pop_msg`, `show_pop`, `lang`)
				VALUES (@typeid, @tagname, @ad_type, @timeset, @start_time, @end_time, @name, @content, @expcontent, @status, @bnr_id, @marq_id, @pop_msg, @show_pop, @lang);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsAdvertDto model)
        {
            string sql = @"UPDATE `cms_advert` SET 
				`typeid` = @typeid,
				`tagname` = @tagname,
				`ad_type` = @ad_type,
				`timeset` = @timeset,
				`start_time` = @start_time,
				`end_time` = @end_time,
				`name` = @name,
				`content` = @content,
				`expcontent` = @expcontent,
				`status` = @status,
				`bnr_id` = @bnr_id,
				`marq_id` = @marq_id,
				`pop_msg` = @pop_msg,
				`show_pop` = @show_pop,
				`lang` = @lang
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_advert` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsAdvertService][Remove]" + ex.Message);
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
