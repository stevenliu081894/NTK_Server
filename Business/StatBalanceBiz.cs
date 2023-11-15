using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.StatBalance;

namespace NTKServer.Business
{
    public class StatBalanceBiz
    {
        #region CRUD
		public static List<StatBalanceList> GetStatBalanceList(StatBalanceFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return WalletRecordService.FindStatBalanceList(whereSql);
        }
       
        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
