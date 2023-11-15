using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.StatTradeAccount;

namespace NTKServer.Business
{
    public class StatTradeAccountBiz
    {
        #region CRUD
		public static List<StatTradeAccountList> GetStatTradeAccountList(StatTradeAccountFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);

			var begin = string.Empty;
			if (filter.start_year.HasValue && filter.start_month.HasValue)
			{
				begin = $"{filter.start_year}/{filter.start_month}/01 00:00:00";
			}
			else if (filter.start_year.HasValue)
			{
				begin = $"{filter.start_year}/01/01 00:00:00";
			}
			else if (filter.start_month.HasValue)
			{
				begin = $"{DateTime.Now.Year}/{filter.start_month}/01 00:00:00";
			}

			if (!string.IsNullOrWhiteSpace(begin))
			{
				whereSql = whereSql.Must($"date('{begin}') <= vw_trade_account.begin_time");
			}

			var end = string.Empty;
			if (filter.end_year.HasValue && filter.end_month.HasValue)
			{
				end = Convert.ToDateTime($"{filter.end_year}/{filter.end_month}/01").AddMonths(1).ToString("yyyy/MM/dd 00:00:00");
			}
			else if (filter.end_year.HasValue)
			{
				end = $"{filter.end_year}/12/31 23:59:59";
			}
			else if (filter.end_month.HasValue)
			{
				end = Convert.ToDateTime($"{DateTime.Now.Year}/{filter.end_month}/01").AddMonths(1).ToString("yyyy/MM/dd 00:00:00");
			}

			if (!string.IsNullOrWhiteSpace(end))
			{
				whereSql = whereSql.Must($" vw_trade_account.end_time < date('{end}')");
			}

			
			return VwTradeAccountService.FindStatTradeAccountList(whereSql);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
