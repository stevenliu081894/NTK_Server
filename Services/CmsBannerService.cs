using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Banner;

namespace DB.Services
{
    public class CmsBannerService
    {
        #region Dto

        public static CmsBannerDto Find(int cms_files_fk)
        {
            string sql = @"SELECT * FROM `cms_banner` WHERE `cms_files_fk` = @cms_files_fk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {cms_files_fk});
                return conn.QueryFirstOrDefault<CmsBannerDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsCarouselService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsBannerDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_banner`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsBannerDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsCarouselService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(CmsBannerDto model)
        {
			string sql = @"INSERT INTO `cms_banner` (
				`cms_files_fk`, `enable`, `sort`, `url`, `size`)
				VALUES (@cms_files_fk, @enable, @sort, @url, @size);";

			try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsCarouselService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int FindPkAfterInsert(CmsBannerDto model)
        {
			string sql = @"INSERT INTO `cms_banner` (
				`cms_files_fk`, `enable`, `sort`, `url`, `size`, `lang`)
				VALUES (@cms_files_fk, @enable, @sort, @url, @size, @lang);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsFilesService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(CmsBannerDto model)
        {
            string sql = @"UPDATE `cms_banner` SET 
				`enable` = @enable,
				`sort` = @sort,
				`url` = @url,
				`size` = @size,
                `cms_files_fk` = @cms_files_fk
				 WHERE `cms_files_fk` = @cms_files_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsCarouselService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int cms_files_fk)
        {
            string sql = @"DELETE `cms_files`, `cms_banner` FROM `cms_banner`
                           INNER JOIN cms_files ON cms_files.pk = cms_banner.cms_files_fk
                           WHERE `cms_files_fk` = @cms_files_fk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { cms_files_fk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsCarouselService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<BannerList> FindBannerList(string whereSql="")
        {
            string sql = @$"SELECT cms_banner.cms_files_fk, cms_banner.size, IF(cms_banner.enable = 1, 'o', 'x') AS enable, cms_banner.lang, 
                cms_banner.sort, CONCAT(cms_banner.url, cms_files.url) AS url, IF(cms_banner.size = 0, '桌機', '手機') AS size_str
                FROM `cms_banner`
                inner join cms_files on cms_files.pk = cms_banner.cms_files_fk {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<BannerList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsBannerService][FindBannerList]" + ex.Message);
                return null;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
