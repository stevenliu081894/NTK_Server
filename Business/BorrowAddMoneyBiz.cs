using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.BorrowAddmoney;


namespace NTKServer.Business
{
	public class BorrowAddmoneyBiz
    {
        #region CRUD
        public static List<BorrowAddmoneyList> GetBorrowAddmoneyList(BorrowAddmoneyFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("borrow_addmoney.status = 0");
            return BorrowAddmoneyService.FindBorrowAddmoneyList(whereSql)?
                .Select(borrowAddmoney => PublicTool.convertUtcToLocalTime(borrowAddmoney)).ToList();
        }

        public static BorrowAddmoneyDto Get(int pk)
        {
            return BorrowAddmoneyService.Find(pk);
        }

        public static void PostCreate(BorrowAddmoneyDto req)
        {

            if (BorrowAddmoneyService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowAddmoneyDto req)
        {
            if (BorrowAddmoneyService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowAddmoneyService.Remove(pk);
        }

        #endregion

        #region ViewModel		
        public static BorrowAddmoneyReview GetReview(int pk)
        {
            return BorrowAddmoneyService.FindBorrowAddmoneyReview(pk);
        }

        /// <summary>
        /// 追加保證金審核
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="verifyStatus"></param>
        /// <returns></returns>
        public static bool ReviewApprove(int pk, bool verifyStatus)
        {
            var borrowAddMoney = BorrowAddmoneyService.Find(pk);
            var vwTradeAccount = TradeMoneyCheckService.GetVwTradeAccountBySubAccount(borrowAddMoney.sub_account);

            //  帳戶不符合追加保證金條件, 保證金總金額不得大於原始保證金
            if (vwTradeAccount.balance + borrowAddMoney.money <= vwTradeAccount.margin * 1.1m + vwTradeAccount.loan_money)
            {
                throw new AppException(3031, "not_match_condition");
            }

            if (borrowAddMoney.status != (int)BorrowAddMoneyVerifyType.Processing)
            {
                throw new AppException(3032, "verify_status_incorrect");
            }

            // 變更狀態
            BorrowAddmoneyService.UpdateStatus(pk, verifyStatus);

            var tempId = verifyStatus ? 38 : 39;

            if (verifyStatus)
            {
                var wallet = WalletService.Find(vwTradeAccount.member_fk);

                WalletLib.MaginCallPass(vwTradeAccount.member_fk,
                    borrowAddMoney.freeze,
                    $"{borrowAddMoney.pk}",
                    vwTradeAccount.sub_account,
                    borrowAddMoney.money,
                    borrowAddMoney.currency);

                // 写入trade_money_record
                var lastRecord = TradeMoneyRecordService.GetByAccount(borrowAddMoney.sub_account);
                TradeMoneyRecordService.Insert(new TradeMoneyRecordDto
                {
                    sub_account = borrowAddMoney.sub_account,
                    member_fk = Convert.ToInt32(borrowAddMoney.member_fk),
                    trade_deal_fk = null,
                    sn = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}",
                    temp_id = tempId,
                    op = 1,
                    currency = borrowAddMoney.currency,
                    balance = lastRecord.wallet_amount,//取最後一筆
                    affect = borrowAddMoney.money,
                    exchange = borrowAddMoney.exchange,
                    wallet_amount = lastRecord.wallet_amount + borrowAddMoney.money,
                    info = "",
                    reviewer = null,
                    create_datetime = DateTime.UtcNow,
                    market = vwTradeAccount.market,
                    stock_code = null,
                    stock_name = null
                });

                // 變更交易帳戶金額
                TradeAccountService.UpdateMoney(borrowAddMoney.sub_account, borrowAddMoney.money);

                MemberTaskLib.MemberTaskFinish(borrowAddMoney.member_fk, 6, "CN"); // 首次补亏
                // 寄送站內信
                //申請單號|交易帳號|保证金|幣別|約當主錢包金額|主錢包幣別
                //你的申請單號:#0#,交易帳號:#1#,追加保证金已经审查通过,管理费:#2##3#(#4##5#)
                SendMessageLib.Send(vwTradeAccount.member_fk, tempId, new object[6] {
                    borrowAddMoney.pk,
                    vwTradeAccount.sub_account,
                    borrowAddMoney.money,
                    borrowAddMoney.currency,
                    borrowAddMoney.freeze,
                    wallet.currency});
            }
            else 
            {
                WalletLib.MaginCallFail(vwTradeAccount.member_fk,
                    borrowAddMoney.freeze,
                    $"{borrowAddMoney.pk}");

                //寄送站內信
                //申請單號|交易帳號
                //你的申請單號:#0#,交易帳號:#1#,返还保证金申請不通過
                SendMessageLib.Send(vwTradeAccount.member_fk, tempId, new object[2] {
                    borrowAddMoney.pk,
                    vwTradeAccount.sub_account
                });
            }

            return true;
        }

		#endregion		
	}
}
