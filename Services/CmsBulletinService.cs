using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Bulletin;

namespace DB.Services
{
    public class CmsBulletinService
    {
        #region Dto

        public static CmsBulletinDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_bulletin` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsBulletinDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBulletinService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsBulletinDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_bulletin`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsBulletinDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBulletinService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsBulletinDto source)
        {
            string sql = @"INSERT INTO `cms_bulletin` (
				`lang`, `title`, `sort`, `on_active`, `view`, `trash`, `starttime`, `endtime`, `topic_content`, `imgid`, `summary`, `outsite`, `url`)
				VALUES (@lang, @title, @sort, @on_active, @view, @trash, @starttime, @endtime, @topic_content, @imgid, @summary, @outsite, @url);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBulletinService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsBulletinDto model)
        {
            string sql = @"UPDATE `cms_bulletin` SET 
				`lang` = @lang,
				`title` = @title,
				`sort` = @sort,
				`on_active` = @on_active,
				`view` = @view,
				`trash` = @trash,
				`starttime` = @starttime,
				`endtime` = @endtime,
				`topic_content` = @topic_content,
				`imgid` = @imgid,
				`summary` = @summary,
				`outsite` = @outsite,
				`url` = @url
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBulletinService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_bulletin` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBulletinService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<BulletinList> FindBulletinList(string whereSql="")
        {
            string sql = @$"SELECT cms_bulletin.pk, cms_bulletin.lang, cms_bulletin.title, cms_bulletin.sort, cms_bulletin.on_active, cms_bulletin.view, cms_bulletin.trash, cms_bulletin.starttime FROM `cms_bulletin`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BulletinList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBulletinService][FindBulletinList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
