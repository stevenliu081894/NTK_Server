using DB.Services;
using Models.Dto;
using Org.BouncyCastle.Ocsp;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.Wallet;
using NTKServer.ViewModels.WalletWithdraw;


namespace NTKServer.Business
{
    public class WalletWithdrawBiz
    {
        #region CRUD
		public static List<WalletWithdrawList> GetWalletWithdrawList(WalletWithdrawFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("wallet_withdraw.status = 0");
            return WalletWithdrawService.FindWalletWithdrawList(whereSql)?
                .Select(walletWithdraw => PublicTool.convertUtcToLocalTime(walletWithdraw)).ToList();
        }

        public static WalletWithdrawDto Get(int pk)
        {
            return WalletWithdrawService.Find(pk);
        }

        public static void PostCreate(WalletWithdrawDto req)
        {
            if (WalletWithdrawService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(WalletWithdrawDto req)
        {
            if( WalletWithdrawService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            WalletWithdrawService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		
        public static WalletWithdrawReview GetReview(int pk)
        {
            return WalletWithdrawService.FindWalletWithdrawReview(pk);
        }


        #endregion

        /// <summary>
        /// 提現審核
        /// </summary>
        /// <param name="req"></param>
        /// <param name="result"></param>
        public static void VerifyWalletWithdraw(WalletWithdrawDto req, bool result)
        {
            var withdraw = WalletWithdrawService.FindWalletWithdrawReview(req.pk);
            var tempId = result ?12:13;

            if (withdraw.status != (int)WalletWithdrawStatus.Processing)
            {
                throw new AppException(3010, "verify_status_incorrect");
            }

            var msgObj = new object[1];
            if (result)
            {
                WalletLib.WithdrawPass(withdraw.member_fk, withdraw.money, withdraw.order_no, withdraw.wallet_amount, withdraw.currency);
				var wallet = WalletService.Find(withdraw.member_fk);
                //申請單號|提现金额|幣別|約當主錢包金額|主錢包幣別
                msgObj = new object[5] {
                    withdraw.order_no,
                    withdraw.money,
                    withdraw.currency,
                    withdraw.wallet_amount,
                    wallet.currency
                };
            }
            else
            {
				WalletLib.WithdrawFail(withdraw.member_fk, withdraw.money, withdraw.order_no);
                msgObj = new object[1] { withdraw.order_no };
			}

            //寄送站內信
            SendMessageLib.Send(withdraw.member_fk, tempId, msgObj);
		}
	}
}
