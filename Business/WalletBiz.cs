using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.UserTradePosition;
using NTKServer.ViewModels.Wallet;
using NTKServer.ViewModels.WalletRecord;

namespace NTKServer.Business
{
    public class WalletBiz
    {
        #region CRUD
		public static List<WalletList> GetWalletList(WalletFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return WalletService.FindWalletList(whereSql)?
                .Select(wallet => PublicTool.convertUtcToLocalTime(wallet)).ToList();
        }

        public static WalletDto Get(int member_fk)
        {
            return WalletService.Find(member_fk);
        }

        public static void PostCreate(WalletDto req)
        {
            if (WalletService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(WalletDto req)
        {
            if( WalletService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int member_fk)
        {
            WalletService.Remove(member_fk);
        }

        #endregion
		
		#region ViewModel		

        public static WalletEditVm GetEditVm(int member_fk)
        {
            return WalletService.FindWalletEditVm(member_fk);
        }

		public static WalletChangeExceptionVm GetChangeException(int member_fk)
		{
            var walletVm= WalletService.FindWalletEditVm(member_fk);
            return new WalletChangeExceptionVm()
            {
                member_fk = walletVm.member_fk,
                account = walletVm.account,
                real_name = walletVm.real_name,
                currency = walletVm.currency,
                balance = walletVm.balance,
                coupon = walletVm.coupon
            };
		}

		#endregion
	}
}
