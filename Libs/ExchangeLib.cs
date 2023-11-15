using NTKServer.Services;

namespace NTKServer.Libs
{
	public class ExchangeLib
	{
		/// <summary>
		/// 取得將 currency_symbol 轉換成 base_symbol 的匯率
		/// </summary>
		/// <param name="currency_symbol"></param>
		/// <param name="base_symbol"></param>
		/// <returns></returns>
		public static decimal GetRate(string currency_symbol, string base_symbol)
		{
			if (currency_symbol == "USDT")
			{
				currency_symbol = "USD";
			}
			if (base_symbol == "USDT")
			{
				base_symbol = "USD";
			}

			decimal factor = 1M;
            if (currency_symbol == "KVND") {
                currency_symbol = "VND";
                factor *= 1000;
            }
            if (base_symbol == "KVND") {
                base_symbol = "VND";
                factor /= 1000;
            }

			decimal rate;
			if (currency_symbol.Equals(base_symbol))
			{
				rate = 1;
			}
			else
			{
				var viewExchangeRateDto = ViewExchangeRateService.GetViewExchangeRate(currency_symbol, base_symbol);
				rate = viewExchangeRateDto.inward_rate * factor;
			}
			return rate;
		}
		/// <summary>
		/// 將金額 amount 從 currency_symbol 轉換成 base_symbol
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="currency_symbol"></param>
		/// <param name="base_symbol"></param>
		/// <returns></returns>
		public static decimal Convert(decimal amount, string currency_symbol, string base_symbol)
		{
			decimal rate = GetRate(currency_symbol, base_symbol);
			return amount * rate;
		}
	}
}
