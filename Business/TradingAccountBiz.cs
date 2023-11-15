using System.Collections.Generic;
using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.TradingAccount;
using NTKServer.Libs;

namespace NTKServer.Business
{
    public class TradingAccountBiz
    {
        #region CRUD

        /// <summary>
        /// 正在交易中的帐号
        /// </summary>
		public static List<TradingAccountList> GetTradingAccountList(TradingAccountFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("status < 3");
            return TradeAccountService.FindTradingAccountList(whereSql)?
                .Select(tradingAccount => PublicTool.convertUtcToLocalTime(tradingAccount)).ToList();
        }

        public static TradeAccountDto Get(string sub_account)
        {
            return TradeAccountService.Find(sub_account);
        }

		public static VwTradeAccountDto GetFromView(string sub_account)
		{
			return VwTradeAccountService.Find(sub_account);
		}

		public static void PostCreate(TradeAccountDto req)
        {
            if (TradeAccountService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(TradeAccountDto req)
        {
            if( TradeAccountService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }


        #endregion
	}
}
