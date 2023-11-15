using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class TradeTemplateService
    {
        #region Dto

        public static TradeTemplateDto Find(int pk)
        {
            string sql = @"SELECT * FROM `trade_template` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.QueryFirstOrDefault<TradeTemplateDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeTemplateService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<TradeTemplateDto> FindAll()
        {
            string sql = @"SELECT * FROM `trade_template`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeTemplateDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeTemplateService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static List<TradeTemplateDto> FindDropDown(string lang)
        {
            string sql = @"SELECT `temp_id`, `name` FROM trade_template
                WHERE `lang` = @lang ";
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<TradeTemplateDto>(sql, new { lang = lang.ToUpper() }).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeTemplateService][FindDropDown]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(TradeTemplateDto source)
        {
            string sql = @"INSERT INTO `trade_template` (
				`temp_id`, `lang`, `name`, `template`, `param`, `demo`)
				VALUES (@temp_id, @lang, @name, @template, @param, @demo);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeTemplateService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int UpdateFull(TradeTemplateDto model)
        {
            string sql = @"UPDATE `trade_template` SET 
				`temp_id` = @temp_id,
				`lang` = @lang,
				`name` = @name,
				`template` = @template,
				`param` = @param,
				`demo` = @demo
				 WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeTemplateService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `trade_template` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeTemplateService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
        #endregion

        #region ViewModel

        #endregion

        #region Other

        public static TradeTemplateDto GetByTempId(int tempId, string lang)
        {
            string sql = @"SELECT * FROM `trade_template` WHERE `temp_id` = @tempId AND `lang` = @lang;";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                var param = DapperMysql.GetParameters(new { tempId, lang });
                return conn.QueryFirstOrDefault<TradeTemplateDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[TradeTemplateService][GetByTempId]" + ex.Message);
                return null;
            }
        }

        #endregion
    }
}
