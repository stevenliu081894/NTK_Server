using Dapper;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Models.Dto;
using NTKServer.ViewModels.ExchangeRate;

namespace NTKServer.Services
{
    public class ViewExchangeRateService
    {
        #region ViewModel
	    public static List<ExchangeRateList> FindExchangeRateList(string whereSql="")
        {
            string sql = @$"SELECT view_exchange_rate.date, view_exchange_rate.currency_symbol, view_exchange_rate.base_symbol, 
							view_exchange_rate.inward_rate, view_exchange_rate.outward_rate, view_exchange_rate.create_time FROM `view_exchange_rate`
							{whereSql}";
            try
            {
                using var conn = DapperMysql.GetReadConnection();
                return conn.Query<ExchangeRateList>(sql).AsList();
            }
            catch (Exception ex)
            {
                LogLib.Log("[ViewExchangeRateService][FindExchangeRateList]" + ex.Message);
                return null;
            }
        }

		public static VwExchangeRateDto GetViewExchangeRate(string currency_symbol, string base_symbol)
		{
			string sql = @"SELECT * FROM `view_exchange_rate` WHERE currency_symbol = @currency_symbol AND base_symbol = @base_symbol ORDER BY create_time DESC";
			var param = new { currency_symbol, base_symbol };
			try
			{
				using (var conn = DapperMysql.GetReadConnection())
				{
					return conn.QueryFirst<VwExchangeRateDto>(sql, param);
				}
			}
			catch (Exception)
			{
				throw new AppException(1040, "read_db_exception");
			}
		}

		#endregion
	}
}
