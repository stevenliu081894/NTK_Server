using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.ReviewBorrow;

namespace NTKServer.Business
{
    public class ReviewBorrowBiz
    {
        #region CRUD
		public static List<ReviewBorrowList> GetReviewBorrowList(ReviewBorrowFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("`borrow`.`status` = -1");
            return BorrowService.FindReviewBorrowList(whereSql)?
                .Select(reviewBorrow => PublicTool.convertUtcToLocalTime(reviewBorrow)).ToList();
        }

        public static BorrowDto Get(int pk)
        {
            return BorrowService.Find(pk);
        }

        public static void PostCreate(BorrowDto req)
        {
            if (BorrowService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(BorrowDto req)
        {
            if( BorrowService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            BorrowService.Remove(pk);
        }

        #endregion

        #region Other

        /// <summary>
        /// 新合約審核
        /// </summary>
        /// <param name="pk"></param>
        /// <param name="input"></param>
        /// <param name="verifyStatus"></param>
        /// <returns></returns>
        public static bool BorrowApplyVerify(int pk, bool verifyStatus, string input = "")
        {
            var borrow = BorrowService.GetBorrowApply(pk);
            var tempId = verifyStatus ? 32 : 33;

            //如果審核日期超過起始日期, 審核就是不通過..狀態改成 3
            if (DateTime.UtcNow.Date > borrow.begin_time.Date)
            {
                // 變更狀態
                BorrowService.UpdateStatus(pk, BorrowStatus.Expired);
                // 寄送站內信
                SendMessageLib.Send(Convert.ToInt32(borrow.member_fk), tempId, new object[2] { pk, "审核日期超过起始日期" });
                //返回訊息
                throw new AppException(3000, "begindate_has_expired");
            }

            if (borrow.status != (int)BorrowStatus.Default)
            {
                //返回訊息
                throw new AppException(3001, "borrow_verify_status_incorrect");
            }

            var wallet = WalletService.Find(borrow.member_fk);

            if (verifyStatus)
            {
                string tradeNum = default;

                //取得國別
                var subAccount = string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    // 随机产生交易号
                    do
                    {
                        tradeNum = PublicTool.GetRandomNum(8);
                        subAccount = $"{borrow.market}{tradeNum}";
                    }
                    while (TradeAccountService.Find(subAccount) is not null);
                }
                else
                {
                    // 检查是否存在
                    subAccount = input;
                    var tradeAccount = TradeAccountService.Find(subAccount);
                    if (tradeAccount is not null)
                        return false;
                }

                var member = MemberService.Find(borrow.member_fk);
                if (string.IsNullOrWhiteSpace(member.sub_account))
                {
                    MemberService.UpdateSubAccount(member.pk, subAccount);
                    //修改客戶端Redis的交易帳號
                    var cacheModel = TokenCatchLib.GetToken(member.token);
                    if(cacheModel is not null) 
                    {
                        cacheModel.sub_account = subAccount;
                        TokenCatchLib.SetToken(member.token, cacheModel);
                    }
                }

                BorrowService.UpdateSubAccount(pk, subAccount);

                // 寫入TradeAccount
                //融资 + 保证金 * borrow_plan  那就需要做
                var warningline = borrow.deposit_money * (Convert.ToDecimal(borrow.warning_line) / 100) + borrow.borrow_money;
                var breakline = borrow.deposit_money * (Convert.ToDecimal(borrow.break_line) / 100) + borrow.borrow_money;

                TradeAccountService.Insert(new TradeAccountDto
                {
                    sub_account = subAccount,
                    member_fk = borrow.member_fk,
                    borrow_plan_fk = borrow.borrow_plan_fk,
                    type = borrow.borrow_type == "free " ? (sbyte)0 : (sbyte)1,
                    market = borrow.market,
                    loan_type = borrow.borrow_type,
                    currency = borrow.currency,
                    mem_money = borrow.init_money,
                    frozen_money = 0,
                    margin = borrow.deposit_money,
                    margin_float = borrow.deposit_money,
                    loan_money = borrow.borrow_money,
                    time_zone = borrow.time_zone,
                    begin_time = borrow.begin_time,
                    end_time = borrow.end_time,
                    close_time = null,
                    status = 0,
                    warningline = warningline,
                    breakline = breakline,
                    notice_warning = null,
                    notice_close = null,
                    multiple = borrow.multiple,
                    borrow_duration = borrow.borrow_duration
                });

                var date = DateTime.Now.ToString("yyyyMMdd");
                var datetime = DateTime.UtcNow;
                var affect = ExchangeLib.Convert(borrow.borrow_money, borrow.currency, wallet.currency);
                //转入金額|幣別|約當主錢包金額|主錢包幣別
                TradeRecordLib.Save(new TradeMoneyRecoreRequest
                {
                    sub_account = subAccount,
                    member_fk = borrow.member_fk,
                    sn = $"{date}{PublicTool.GetRandomNum(6)}",
                    currency = borrow.currency,
                    create_datetime = datetime,
                    wallet_amount = borrow.init_money + borrow.borrow_money,
                    affect = borrow.borrow_money,
                    temp_id = 32,
                    list = new object[4] { 
                        borrow.borrow_money,
                        borrow.currency,
                        affect,
                        wallet.currency,
                    }
                });

                affect = ExchangeLib.Convert(borrow.borrow_money * borrow.multiple, borrow.currency, wallet.currency);
                //转入金額|幣別|約當主錢包金額|主錢包幣別
                TradeRecordLib.Save(new TradeMoneyRecoreRequest
                {
                    sub_account = subAccount,
                    member_fk = borrow.member_fk,
                    sn = $"{date}{PublicTool.GetRandomNum(6)}",
                    currency = borrow.currency,
                    create_datetime = datetime,
                    wallet_amount = borrow.init_money + borrow.borrow_money * borrow.multiple,
                    affect = borrow.borrow_money * borrow.multiple,
                    temp_id = 201,
                    list = new object[4] {
                        borrow.borrow_money * borrow.multiple,
                        borrow.currency,
                        affect,
                        wallet.currency,
                    }
                });

                //寫入Borrow_fee
                //管理費要轉換成 "主錢包"的幣別..他那個是交易錢包的幣別 => 調用共用方法
                var borrowFee = ExchangeLib.Convert(borrow.borrow_interest, borrow.currency, wallet.currency);
                var totalCoupon = ExchangeLib.Convert(borrow.total_coupon, borrow.currency, wallet.currency);
                var totalFee = ExchangeLib.Convert(borrow.total_fee, borrow.currency, wallet.currency);
                BorrowFeeService.Insert(new BorrowFeeDto()
                {
                    member_fk = member.pk,
                    sub_account = subAccount,
                    borrow_fk = borrow.pk,
                    type = (int)BorrowTypeType.NewContract,
                    borrow_fee = borrowFee,
                    use_coupon = totalCoupon,
                    fee_received = totalFee,
                    borrow_duration = borrow.borrow_duration,
                    create_time = datetime
                });

                // 變更狀態
                BorrowService.UpdateStatus(pk, BorrowStatus.Using);

                if (borrow.borrow_interest > 0 && borrow.borrow_type == "trial")
                {
                    MemberTaskLib.MemberTaskFinish(borrow.member_fk, 4, "CN"); // 首次建立交易号
                }

                var depositMoney = ExchangeLib.Convert(borrow.deposit_money, borrow.currency, wallet.currency);
                WalletLib.NewBorrowPass(borrow.member_fk,
                    depositMoney,
                    $"{borrow.pk}",
                    subAccount,
                    borrow.deposit_money,
                    wallet.currency);
                TradeDealService.FindPkAfterInsert(new TradeDealDto
                {
                    sub_account = subAccount,
                    market = borrow.market,
                    create_datetime = datetime,
                    currency = borrow.currency,
                    other_fee = depositMoney,
                    trade_order_sn = borrow.order_id,
                    stock_code = "",
                    stock_name = "",
                    order_type = 0,
                    dir = 0,
                    final_price = 0,
                    final_volume = 0,
                    total_pay = 0,
                    total_amount = 0,
                    total_cost = 0,
                    handling_fee = 0,
                    transfer_fee = 0,
                    stamp_fee = 0,
                });

                //修改recommend_register
                //borrow.member_fk 對應 recommend_register.invitee_fk

                var recommendRegister = RecommendRegisterService.Find(borrow.member_fk);
                if (recommendRegister is not null && !recommendRegister.first_borrow_date.HasValue && borrow.borrow_type != "trial")
                {
                    RecommendRegisterService.UpdateFirstBorrowDate(recommendRegister);
				}

				// 寄送站內信
				SendMessageLib.Send(Convert.ToInt32(borrow.member_fk), tempId, new object[6] {
                    pk,
                    subAccount,
                    depositMoney,
                    borrow.currency,
                    borrow.init_money,
                    wallet.currency
                });
            }
            else
            {
                var depositMoney = ExchangeLib.Convert(borrow.deposit_money, borrow.currency, wallet.currency);
                WalletLib.NewBorrowFail(borrow.member_fk, depositMoney, $"{borrow.pk}");
                // 變更狀態
                BorrowService.UpdateStatus(pk, BorrowStatus.Forbid);
                // 寄送站內信
                SendMessageLib.Send(Convert.ToInt32(borrow.member_fk), tempId, new object[2] { pk, "审核不通过" });
            }
            return true;
        }

        #endregion
    }
}
