using DB.Services;
using Models.Dto;
using Org.BouncyCastle.Ocsp;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.TradeMoneyCheck;

namespace NTKServer.Business
{
    public class TradeMoneyCheckBiz
    {
        #region CRUD
		public static List<TradeMoneyCheckList> GetTradeMoneyCheckList(TradeMoneyCheckFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("trade_money_check.type = 0")
                .Must("trade_money_check.state = 0");
            return TradeMoneyCheckService.FindTradeMoneyCheckList(whereSql);
        }

        public static TradeMoneyCheckDto Get(int pk)
        {
            return TradeMoneyCheckService.Find(pk);
        }

        public static void PostCreate(TradeMoneyCheckDto req)
        {
            if (TradeMoneyCheckService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(TradeMoneyCheckDto req)
        {
            if( TradeMoneyCheckService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            TradeMoneyCheckService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		
        public static TradeMoneyCheckReview GetReview(int pk)
        {
            return TradeMoneyCheckService.FindTradeMoneyCheckReview(pk);
        }

        #endregion

        #region Other

        /// <summary>
        /// 申請提盈審核
        /// </summary>
        /// <returns></returns>
        public static bool WithdrawVerify(int pk, bool verifyStatus)
        {
            var tradeMoneyCheck = TradeMoneyCheckService.Find(pk);
            var vwTradeAccount = TradeMoneyCheckService.GetVwTradeAccountBySubAccount(tradeMoneyCheck.sub_account);

            // 余额不足，不允许提盈
            if (vwTradeAccount.balance - tradeMoneyCheck.frozen <= (vwTradeAccount.warningline + vwTradeAccount.breakline) / 2)
            {
                throw new AppException(3020, "not_enough_trade_money");
            }

            if (tradeMoneyCheck.state != (int)VerifyStatusType.Processing)
            {
                throw new AppException(3021, "verify_withdraw_status_incorrect");
            }

            // 更新账户金额及冻结金额
            TradeAccountService.UpdateTradeAccountVolume(verifyStatus, tradeMoneyCheck.sub_account, tradeMoneyCheck.frozen);

            // 删除冻结记录
            TradeFrozenService.DelTradeFrozen(tradeMoneyCheck.pk);

            // 变更状态/审核时间
            TradeMoneyCheckService.UpdateWithdraw(Convert.ToInt32(pk),
               verifyStatus ? VerifyStatusType.Successful : VerifyStatusType.Fail);

            var tempId = verifyStatus ? 49 : 50;

            if (verifyStatus)
            {
                // 写入trade_money_record
                var lastRecord = TradeMoneyRecordService.GetByAccount(tradeMoneyCheck.sub_account);
                TradeMoneyRecordService.Insert(new TradeMoneyRecordDto
                {
                    sub_account = tradeMoneyCheck.sub_account,
                    member_fk = Convert.ToInt32(vwTradeAccount.member_fk),
                    trade_deal_fk = null,
                    sn = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}",
                    temp_id = tempId,
                    op = 1,
                    currency = tradeMoneyCheck.currency,
                    balance = lastRecord.wallet_amount,//取最後一筆
                    affect = tradeMoneyCheck.frozen,
                    exchange = tradeMoneyCheck.exchange,
                    wallet_amount = lastRecord.wallet_amount - tradeMoneyCheck.amount,
                    info = "",
                    reviewer = null,
                    create_datetime = DateTime.UtcNow,
                    market = vwTradeAccount.market,
                    stock_code = null,
                    stock_name = null
                });

                var wallet = WalletService.Find(vwTradeAccount.member_fk);
                WalletLib.WithdrawTradePass(
                   vwTradeAccount.member_fk,
                   tradeMoneyCheck.sn,
                   tradeMoneyCheck.amount,
                   tradeMoneyCheck.sn,
                   tradeMoneyCheck.frozen,
                   tradeMoneyCheck.currency);

                var currencyAmount = ExchangeLib.Convert(tradeMoneyCheck.amount, tradeMoneyCheck.currency, wallet.currency);
                //申請單號|交易帳號|提盈金額|幣別|約當主錢包金額|主錢包幣別
                SendMessageLib.Send(
                    vwTradeAccount.member_fk, 
                    tempId, 
                    new object[6] {
                    tradeMoneyCheck.sn,
                    tradeMoneyCheck.sub_account,
                    tradeMoneyCheck.frozen,
                    tradeMoneyCheck.currency,
                    tradeMoneyCheck.amount,
                    wallet.currency});
            }
            else
            {
                //申請單號
                SendMessageLib.Send(
                    vwTradeAccount.member_fk,
                    tempId,
                    new object[2] {
                        tradeMoneyCheck.sn,
                        tradeMoneyCheck.sub_account
                    });
            }

            return true;
        }

        #endregion
    }
}
