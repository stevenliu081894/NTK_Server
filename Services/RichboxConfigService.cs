using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class RichboxConfigService
    {
        #region Dto

        public static RichboxConfigDto Find(int id)
        {
            string sql = @"SELECT * FROM `richbox_config` WHERE `id` = @id";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
				var param = DapperMysql.GetParameters(new {id});
                return conn.QueryFirstOrDefault<RichboxConfigDto>(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxConfigService][Find]" + ex.Message);
                return null;
            }
        }

        public static List<RichboxConfigDto> FindAll()
        {
            string sql = @"SELECT * FROM `richbox_config`";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<RichboxConfigDto>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxConfigService][FindAll]" + ex.Message);
                return null;
            }
        }

        public static int FindPkAfterInsert(RichboxConfigDto source)
        {
            string sql = @"INSERT INTO `richbox_config` (
				`enable`, `active_date`, `diactive_date`, `currency`, `min_investment`, `max_investment`, `interest_rate`, `begin_profit`, `closing_time`, `give_interest_time`, `feature`, `description`, `trade_info`)
				VALUES (@enable, @active_date, @diactive_date, @currency, @min_investment, @max_investment, @interest_rate, @begin_profit, @closing_time, @give_interest_time, @feature, @description, @trade_info);

                select @@IDENTITY;";
            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.ExecuteScalar<int>(sql, source);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxConfigService][FindPkAfterInsert]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }
		
        public static int UpdateFull(RichboxConfigDto model)
        {
            string sql = @"UPDATE `richbox_config` SET 
				`enable` = @enable,
				`active_date` = @active_date,
				`diactive_date` = @diactive_date,
				`currency` = @currency,
				`min_investment` = @min_investment,
				`max_investment` = @max_investment,
				`interest_rate` = @interest_rate,
				`begin_profit` = @begin_profit,
				`closing_time` = @closing_time,
				`give_interest_time` = @give_interest_time,
				`feature` = @feature,
				`description` = @description,
				`trade_info` = @trade_info
				 WHERE `id` = @id";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                return conn.Execute(sql, model);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxConfigService][UpdateFull]" + ex.Message);
                throw new AppException(1030, "write_db_exception");
            }
        }

        public static int Remove(int id)
        {
            string sql = @"DELETE FROM `richbox_config` WHERE `id` = @id";

            try
            {
                using var conn = DapperMysql.GetWriteConntion();
                var param = DapperMysql.GetParameters(new { id });
                return conn.Execute(sql, param);
            }
            catch (Exception ex)
            {
                LogLib.Log("[RichboxConfigService][Remove]" + ex.Message);
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
