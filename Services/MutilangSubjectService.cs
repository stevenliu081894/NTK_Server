using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.MultiLang;

namespace DB.Services
{
    public class MutilangSubjectService
    {
        #region Dto

        public static MutilangSubjectDto Find(string lang)
        {
            string sql = @"SELECT * FROM `mutilang_subject` WHERE `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {lang});
                return conn.QueryFirstOrDefault<MutilangSubjectDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangSubjectService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<MutilangSubjectDto> FindAll()
        {
            string sql = @"SELECT * FROM `mutilang_subject`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<MutilangSubjectDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangSubjectService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int Insert(MutilangSubjectDto model)
        {
			string sql = @"INSERT INTO `mutilang_subject` (
				`lang`, `title`, `enable`, `icon`, `admin_default`, `app_default`)
				VALUES (@lang, @title, @enable, @icon, @admin_default, @app_default); ";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangSubjectService][Insert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(MutilangSubjectDto model)
        {
            string sql = @"UPDATE `mutilang_subject` SET 
				`title` = @title,
				`enable` = @enable,
				`icon` = @icon,
				`admin_default` = @admin_default,
				`app_default` = @app_default
				 WHERE `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangSubjectService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(string lang)
        {
            string sql = @"DELETE FROM `mutilang_subject` WHERE `lang` = @lang";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { lang });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[MutilangSubjectService][Remove]" + ex.Message);
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
