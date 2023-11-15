using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using Models.Dto;

namespace DB.Services
{
    public class StatisticsService
    {
        #region Dto
	    public static int Count(string table_name, string whereSql="")
        {
            string sql = @$"SELECT COUNT(*) FROM `{table_name}` {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.ExecuteScalar<int>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StatisticsService][Count]" + ex.Message);
                return 0;
            }
        }

	    public static int Count(string table_name, string join, string on, string whereSql="")
        {
            string sql = @$"SELECT COUNT(*) FROM `{table_name}` INNER JOIN {join} ON {on} {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.ExecuteScalar<int>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StatisticsService][JOIN Count]" + ex.Message);
                return 0;
            }
        }

	    public static int Sum(string table_name, string column, string whereSql="")
        {
            string sql = @$"SELECT SUM({column}) FROM `{table_name}` {whereSql}";

            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.ExecuteScalar<int>(sql);
            }
            catch (Exception ex)
            {
                LogLib.Log("[StatisticsService][Sum]" + ex.Message);
                return 0;
            }
        }

        #endregion

        #region Other

        #endregion
    }
}
