using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.RequestRenew;

namespace NTKServer.Business
{
    public class RequestRenewBiz
    {
        #region CRUD
		public static List<RequestRenewList> GetRequestRenewList(RequestRenewFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("borrow_request.type = 1")
                .Must("borrow_request.status = 0");            
            return BorrowRequestService.FindRequestRenewList(whereSql)?
                .Select(findRequest => PublicTool.convertUtcToLocalTime(findRequest)).ToList();
        }

        public static BorrowRequestDto Get(int pk)
        {
            return BorrowRequestService.Find(pk);
        }

        public static void PostCreate(BorrowRequestDto req)
        {
            if (BorrowRequestService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowRequestDto req)
        {
            if( BorrowRequestService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowRequestService.Remove(pk);
        }

        #endregion
		
		#region ViewModel
        public static RequestRenewReview GetReview(int pk)
        {
            return BorrowRequestService.FindRequestRenewReview(pk);
        }


        #endregion

        #region Other

        /// <summary>
        /// 續期審核
        /// </summary>
        /// <returns></returns>
        public static void RenewalVerify(int borrowRequestId, bool verifyStatus)
        {
            var borrowRequest = BorrowRequestService.Find(borrowRequestId);

            if (borrowRequest.status != (int)VerifyStatusType.Processing)
            {
                throw new AppException(3010, "verify_status_incorrect");
            }

            var tradeAccount = TradeAccountService.Find(borrowRequest.sub_account);

            //修改狀態及審核時間
            BorrowRequestService.UpdateState(borrowRequestId, verifyStatus);

            //borrow_request的數據添加到borrow_fee這張
            //管理費要轉換成 "主錢包"的幣別..他那個是交易錢包的幣別 => 調用共用方法
            var wallet = WalletService.Find(borrowRequest.member_fk);
            BorrowFeeService.Insert(new BorrowFeeDto
            {
                sub_account = borrowRequest.sub_account,
                member_fk = borrowRequest.member_fk,
                borrow_fk = borrowRequest.pk,
                type = (int)BorrowTypeType.Renew,
                borrow_fee = borrowRequest.borrow_fee,
                use_coupon = borrowRequest.use_coupon,
                fee_received = borrowRequest.fee_received,
                borrow_duration = borrowRequest.borrow_duration,
                create_time = DateTime.UtcNow
            });

            //寄送站內信，續期審核/成功/失敗 41 - 43
            var tempId = verifyStatus ? 42 : 43;

            //成功
            if (verifyStatus)
            {
				if (tradeAccount.mem_money - tradeAccount.frozen_money <= tradeAccount.breakline)
				{
					throw new AppException(3041, "trade_account_balance_not_enough");
				}

				WalletLib.BorrowRenewalPass(
                    borrowRequest.member_fk, 
                    borrowRequest.fee_received, 
                    $"{borrowRequest.pk}",
					borrowRequest.sub_account);

				//扣款管理費
				WalletLib.RenewalManagementFee(
                    borrowRequest.member_fk,
                    borrowRequest.fee_received,
                    borrowRequest.sub_account);

                //僅成功修改交易帳號新的到期日及狀態
                TradeAccountService.UpdateAccountRenewal(borrowRequest.sub_account, AccountStatusType.Tradable, borrowRequest.new_end_time);
                //申請單號|交易帳號|金額|幣別|約當主錢包金額|主錢包幣別
                SendMessageLib.Send(borrowRequest.member_fk, tempId, new object[6]{
                    borrowRequest.pk,
                    borrowRequest.sub_account,
                    borrowRequest.fee_received,
                    wallet.currency,
                    borrowRequest.fee_received,
                    wallet.currency
                });
            }
            else
            {
                WalletLib.BorrowRenewalFail(borrowRequest.member_fk, borrowRequest.fee_received, $"{borrowRequest.pk}");

				//申請單號|交易帳號
				SendMessageLib.Send(borrowRequest.member_fk, tempId, new object[2] {
                    borrowRequest.pk,
                    borrowRequest.sub_account
                });
            }

        }

        #endregion
    }
}
