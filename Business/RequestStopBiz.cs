using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.RequestStop;

namespace NTKServer.Business
{
    public class RequestStopBiz
    {
        #region CRUD
		public static List<RequestStopList> GetRequestStopList(RequestStopFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("borrow_request.type = 2")
                .Must("borrow_request.status = 0");
            return BorrowRequestService.FindRequestStopList(whereSql)?
                .Select(requestStop => PublicTool.convertUtcToLocalTime(requestStop)).ToList();
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
        public static RequestStopReview GetReview(int pk)
        {
            return BorrowRequestService.FindRequestStopReview(pk);
        }

        #endregion

        #region Other

        /// <summary>
        /// 終止交易審核
        /// </summary>
        /// <returns></returns>
        public static void TerminateVerify(int adminId, int borrowRequestId, bool verifyStatus)
        {
            var borrowRequest = BorrowRequestService.Find(borrowRequestId);

            if (borrowRequest.status != (int)VerifyStatusType.Processing)
            {
                throw new AppException(3010, "verify_status_incorrect");
            }

            //修改狀態及審核時間
            BorrowRequestService.UpdateState(borrowRequestId, verifyStatus);

            if (verifyStatus)
            {
                //判斷是否清空
                var stockPosition = TradePositionService.GetPositionBySubAccount(borrowRequest.sub_account);

                if (stockPosition > 0)
                {
                    throw new AppException(3000, "stock_not_position");
                }

                //修改子帳號關閉帳號時間及狀態
                TradeAccountService.InAdvanceClose(borrowRequest.sub_account, borrowRequest.new_end_time);

                //重計配息表
                BorrowService.InAdvanceReset(borrowRequest.pk, borrowRequest.sub_account);
            }

            //寄送站內信
            var tempId = verifyStatus ? 53 : 54;
            SendMessageLib.Send(adminId, tempId, new object[1] { borrowRequest.pk });
        }

        #endregion
    }
}
