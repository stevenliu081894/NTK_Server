using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.EndTradeAccount;

namespace NTKServer.Business
{
    public class EndTradeAccountBiz
    {
        #region CRUD
		public static List<EndTradeAccountList> GetEndTradeAccountList(EndTradeAccountFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return VwTradeAccountService.FindEndTradeAccountList(whereSql)?
                .Select(endTradeAccount => PublicTool.convertUtcToLocalTime(endTradeAccount)).ToList();
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
