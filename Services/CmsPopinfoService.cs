using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.CmsPopinfo;

namespace DB.Services
{
    public class CmsPopinfoService
    {
        #region Dto

        public static CmsPopinfoDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_popinfo` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsPopinfoDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPopinfoService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsPopinfoDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_popinfo`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsPopinfoDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPopinfoService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsPopinfoDto source)
        {
            string sql = @"INSERT INTO `cms_popinfo` (
				`lang`, `info`, `size`, `enable`)
				VALUES (@lang, @info, @size, @enable);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPopinfoService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsPopinfoDto model)
        {
            string sql = @"UPDATE `cms_popinfo` SET 
				`lang` = @lang,
				`info` = @info,
				`size` = @size,
				`enable` = @enable
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPopinfoService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_popinfo` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPopinfoService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<CmsPopinfoList> FindCmsPopinfoList(string whereSql="")
        {
            string sql = @$"SELECT cms_popinfo.pk, cms_popinfo.lang, cms_popinfo.info, cms_popinfo.size, cms_popinfo.enable FROM `cms_popinfo`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsPopinfoList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsPopinfoService][FindCmsPopinfoList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
