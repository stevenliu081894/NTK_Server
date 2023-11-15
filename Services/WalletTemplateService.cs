using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;
using NTKServer.ViewModels.WalletTemplate;

namespace DB.Services
{
    public class WalletTemplateService
    {
        #region Dto

        public static WalletTemplateDto Find(int pk)
        {
            string sql = @"SELECT * FROM `wallet_template` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {pk});
                return conn.QueryFirstOrDefault<WalletTemplateDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTemplateService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<WalletTemplateDto> FindAll()
        {
            string sql = @"SELECT * FROM `wallet_template`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletTemplateDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTemplateService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(WalletTemplateDto source)
        {
            string sql = @"INSERT INTO `wallet_template` (
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
                LogLib.Log("[WalletTemplateService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(WalletTemplateDto model)
        {
            string sql = @"UPDATE `wallet_template` SET 
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
                LogLib.Log("[WalletTemplateService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int pk)
        {
            string sql = @"DELETE FROM `wallet_template` WHERE `pk` = @pk";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { pk });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTemplateService][Remove]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static WalletTemplateDto GetByTempId(int temp_id, string lang)
        {
            try
            {
                using (var conn = DapperMysql.GetReadConnection())
                {
                    string strSql = @"SELECT * FROM wallet_template WHERE temp_Id = @temp_Id and Lang = @Lang;";
                    var param = new { temp_Id = temp_id, Lang = lang };

                    return conn.QuerySingleOrDefault<WalletTemplateDto>(strSql, param);
                }
            }
            catch (Exception e)
            {
                throw new AppException(1040, "read_db_exception");
            }
        }
        #endregion

        #region ViewModel
	    public static List<WalletTemplateList> FindWalletTemplateList(string whereSql="")
        {
            string sql = @$"SELECT wallet_template.pk, wallet_template.temp_id, wallet_template.lang, wallet_template.name, wallet_template.template, wallet_template.param, wallet_template.demo FROM `wallet_template`{whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<WalletTemplateList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTemplateService][FindWalletTemplateList]" + ex.Message);
                return null;
            }
        }



        public static WalletTemplateEditVm FindWalletTemplateEditVm(int pk)
        {
            string sql = @$"SELECT wallet_template.pk, wallet_template.temp_id, wallet_template.lang, wallet_template.name, wallet_template.template, wallet_template.param, wallet_template.demo FROM `wallet_template`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.QueryFirstOrDefault<WalletTemplateEditVm>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[WalletTemplateService][FindWalletTemplateEditVm]" + ex.Message);
                return null;
            }
        }




        #endregion

        #region Other

        #endregion
    }
}
