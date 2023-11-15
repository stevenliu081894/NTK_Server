using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.HisWalletRecharge;

namespace NTKServer.Business
{
    public class HisWalletRechargeBiz
    {
        #region CRUD
		public static List<HisWalletRechargeList> GetHisWalletRechargeList(HisWalletRechargeFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("wallet_recharge.status > 0");
            return WalletRechargeService.FindHisWalletRechargeList(whereSql)?
                .Select(hisAddmoney => PublicTool.convertUtcToLocalTime(hisAddmoney)).ToList();
        }

        public static WalletRechargeDto Get(int pk)
        {
            return WalletRechargeService.Find(pk);
        }

        public static void PostCreate(WalletRechargeDto req)
        {
            if (WalletRechargeService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(WalletRechargeDto req)
        {
            if( WalletRechargeService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            WalletRechargeService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
