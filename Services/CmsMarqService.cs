using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.Marquee;

namespace DB.Services
{
    public class CmsMarqService
    {
        #region Dto

        public static CmsMarqDto Find(int pk)
        {
            string sql = @"SELECT * FROM `cms_marq` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<CmsMarqDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMarqService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<CmsMarqDto> FindAll()
        {
            string sql = @"SELECT * FROM `cms_marq`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<CmsMarqDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMarqService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(CmsMarqDto source)
        {
            string sql = @"INSERT INTO `cms_marq` (
				`lang`, `enable`, `msg`)
				VALUES (@lang, @enable, @msg);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMarqService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(CmsMarqDto model)
        {
            string sql = @"UPDATE `cms_marq` SET 
				`lang` = @lang,
				`enable` = @enable,
				`msg` = @msg
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMarqService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `cms_marq` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMarqService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<MarqueeList> FindMarqueeList(string whereSql="")
        {
            string sql = @$"SELECT cms_marq.pk, cms_marq.lang, cms_marq.enable, cms_marq.msg FROM `cms_marq`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MarqueeList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[CmsMarqService][FindMarqueeList]" + ex.Message);
                return null;
            }
        }







        #endregion

        #region Other

        #endregion
    }
}
