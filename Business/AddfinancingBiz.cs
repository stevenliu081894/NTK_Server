using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.Addfinancing;
using NTKServer.ViewModels.Wallet;

namespace NTKServer.Business
{
    public class AddfinancingBiz
    {
        #region CRUD
		public static List<AddfinancingList> GetAddfinancingList(AddfinancingFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("borrow_addfinancing.status = 0");
            return BorrowAddfinancingService.FindAddfinancingList(whereSql)?
                .Select(addfinancing => PublicTool.convertUtcToLocalTime(addfinancing)).ToList();
        }

        public static BorrowAddfinancingDto Get(int pk)
        {
            return BorrowAddfinancingService.Find(pk);
        }

        public static void PostCreate(BorrowAddfinancingDto req)
        {
            if (BorrowAddfinancingService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowAddfinancingDto req)
        {
            if( BorrowAddfinancingService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowAddfinancingService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		
        public static AddfinancingReview GetReview(int pk)
        {
            return BorrowAddfinancingService.FindAddfinancingReview(pk);
        }

        public static AddfinancingEditVm GetEditVm(int pk)
        {
            return BorrowAddfinancingService.FindAddfinancingEditVm(pk);
        }

        #endregion

        #region Other

        /// <summary>
        /// 擴大融資審核
        /// </summary>
        /// <returns></returns>
        public static void ExpandBorrowVerify(int id, bool verifyStatus)
        {
            var borrowAddfinancing = BorrowAddfinancingService.Find(id);
            var tradeAccount = TradeAccountService.Find(borrowAddfinancing.sub_account);

            if (borrowAddfinancing.status != (int)VerifyStatusType.Processing)
            {
                throw new AppException(3010, "verify_status_incorrect");
            }

            //修改狀態及審核時間
            BorrowAddfinancingService.UpdateState(id, verifyStatus);

            #region 轉換金額

            var wallet = WalletService.Find(borrowAddfinancing.member_fk);
            var affectMoney = ExchangeLib.Convert(
                  borrowAddfinancing.money,
                  borrowAddfinancing.currency,
                  wallet.currency);

            #endregion

            var tempId = verifyStatus ? 45 : 46;
            if (verifyStatus)
            {
                //依照配資類型，取平倉/預警設置
                var borrowType = tradeAccount.loan_type == "day" ? "day_loss"
                    : tradeAccount.loan_type == "week" ? "week_loss"
                    : tradeAccount.loan_type == "month" ? "month_loss"
                    : tradeAccount.loan_type == "vip" ? "vip_loss"
                    : "free_loss";

                var config = AdminConfigService.Find(borrowType);
                var loss = config.value.Split("|");
                TradeAccountService.UpdateExpandBorrow(
                    borrowAddfinancing.sub_account, 
                    borrowAddfinancing.money, 
                    borrowAddfinancing.multiple, 
                    Convert.ToDecimal(loss[0]), 
                    Convert.ToDecimal(loss[1]));

                #region 写入兩筆 trade_money_record

                var lastRecord = TradeMoneyRecordService.GetByAccount(borrowAddfinancing.sub_account);
                //添加扩大融资保证金
                TradeMoneyRecordService.Insert(new TradeMoneyRecordDto
                {
                    sub_account = borrowAddfinancing.sub_account,
                    member_fk = borrowAddfinancing.member_fk,
                    trade_deal_fk = null,
                    sn = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}",
                    temp_id = tempId,
                    op = 1,
                    currency = borrowAddfinancing.currency,
                    balance = lastRecord.wallet_amount,//取最後一筆
                    affect = borrowAddfinancing.money,
                    exchange = borrowAddfinancing.exchange,
                    wallet_amount = affectMoney,//balance + affect
                    info = "",
                    reviewer = null,
                    create_datetime = DateTime.UtcNow,
                    market = tradeAccount.market,
                    stock_code = null,
                    stock_name = null
                });

                //添加扩大融资融资金额
                var money = borrowAddfinancing.money * borrowAddfinancing.multiple;
                var affectDepositMoney = ExchangeLib.Convert(
                 money,
                 borrowAddfinancing.currency,
                 wallet.currency);
                TradeMoneyRecordService.Insert(new TradeMoneyRecordDto
                {
                    sub_account = borrowAddfinancing.sub_account,
                    member_fk = borrowAddfinancing.member_fk,
                    trade_deal_fk = null,
                    sn = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}",
                    temp_id = tempId,
                    op = 1,
                    currency = borrowAddfinancing.currency,
                    balance = lastRecord.wallet_amount + borrowAddfinancing.money,//取最後一筆
                    affect = money,
                    exchange = borrowAddfinancing.exchange,
                    wallet_amount = affectDepositMoney,//balance + affect
                    info = "",
                    reviewer = null,
                    create_datetime = DateTime.UtcNow,
                    market = tradeAccount.market,
                    stock_code = null,
                    stock_name = null
                });

                #endregion

                WalletLib.ExpandBorrowPass(
                    borrowAddfinancing.member_fk,
                    affectMoney,
                    Convert.ToString(borrowAddfinancing.pk),
                    borrowAddfinancing.sub_account,
                    borrowAddfinancing.money,
                    borrowAddfinancing.currency);

                //寄送站內信，擴大融資審核/成功/失敗  45
                //申請單號|交易帳號|約當主錢包金額|主錢包幣別|金額|幣別
                SendMessageLib.Send(borrowAddfinancing.member_fk, tempId, new object[6]
                    {
                        borrowAddfinancing.pk,
                        borrowAddfinancing.sub_account,
                        affectMoney,
                        wallet.currency,
                        borrowAddfinancing.money,
                        borrowAddfinancing.currency
                    });
            }
            else
            {
                WalletLib.ExpandBorrowFail(
                    borrowAddfinancing.member_fk,
                    affectMoney,
                    Convert.ToString(borrowAddfinancing.pk));

                //申請單號|交易帳號
                //寄送站內信，擴大融資審核/成功/失敗 46
                SendMessageLib.Send(borrowAddfinancing.member_fk, tempId, new object[2] 
                {
                    borrowAddfinancing.pk,
                    borrowAddfinancing.sub_account
                });
            }

           
        }

        #endregion
    }
}
