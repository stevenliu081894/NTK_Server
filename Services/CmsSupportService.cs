using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.CmsSupport;

namespace DB.Services
{
    public class CmsSupportService
    {
        #region Dto

        public static CmsSupportDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_support` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsSupportDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsSupportService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsSupportDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_support`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsSupportDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsSupportService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsSupportDto source)
        {
            string sql = @"INSERT INTO `cms_support` (
				`lang`, `svc_phone`, `svc_workday`, `svc_nonworkday`, `svc_email`, `svc_link`)
				VALUES (@lang, @svc_phone, @svc_workday, @svc_nonworkday, @svc_email, @svc_link);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsSupportService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsSupportDto model)
        {
            string sql = @"UPDATE `cms_support` SET 
				`lang` = @lang,
				`svc_phone` = @svc_phone,
				`svc_workday` = @svc_workday,
				`svc_nonworkday` = @svc_nonworkday,
				`svc_email` = @svc_email,
				`svc_link` = @svc_link
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsSupportService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_support` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsSupportService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<CmsSupportList> FindCmsSupportList(string whereSql="")
        {
            string sql = @$"SELECT cms_support.pk, cms_support.lang, cms_support.svc_phone, cms_support.svc_workday, cms_support.svc_nonworkday, cms_support.svc_email, cms_support.svc_link FROM `cms_support`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsSupportList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsSupportService][FindCmsSupportList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
