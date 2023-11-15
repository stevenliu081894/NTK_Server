using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Services;
using NTKServer.Tool;
using NTKServer.ViewModels.ExchangeRate;

namespace NTKServer.Business
{
    public class ExchangeRateBiz
    {
        #region CRUD
		public static List<ExchangeRateList> GetExchangeRateList(ExchangeRateFilter? filter)
        {
            if (filter.begin_time.HasValue)
            {
                filter.begin_time = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(filter.begin_time));
			}
			if (filter.end_time.HasValue)
			{
				filter.end_time = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(filter.end_time));
			}

			string whereSql = SqlTool.Build(filter);
            return ViewExchangeRateService.FindExchangeRateList(whereSql)?
                .Select(exchangeRate => PublicTool.convertUtcToLocalTime(exchangeRate)).ToList();
        }

         #endregion
		
		#region ViewModel		


		#endregion		
	}
}
