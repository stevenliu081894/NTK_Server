using DB.Services;
using Models.Dto;
using NTKServer.Models.Wallet;

namespace NTKServer.Libs
{
    public class WalletLib
    {


        public WalletLib()
        {

        }

        /// <summary>
        /// 1	充值成功
        /// </summary>
        public static bool RechargePass(int member, decimal change, string id, decimal org_money, string org_currency, decimal exchange)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 因為是增加所以不用檢查錢包帳戶金額，但必須檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, change);

            // 3. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 4. 写入到 钱包纪录
            // 申請單號|金额|币别|原金额|原币别
            object[]? list = new object[5];
            list[0] = id;
            list[1] = change;
            list[2] = ConfigLib.Get("wallet_currency");
            list[3] = org_money;
            list[4] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 1;
            record.subtype = 1;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 1;
            record.affect = change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 49	提盈成功
        /// </summary>
        public static bool WithdrawTradePass(int member, string sn, decimal change, string id, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 因為是增加所以不用檢查錢包帳戶金額，但必須檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, change);

            // 3. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 4. 写入到 钱包纪录
            //交易帳號|申请单号|主錢包金額|主錢包幣別|转入金額|幣別
            object[]? list = new object[6];
            list[0] = id;
            list[1] = sn;
            list[2] = change;
            list[3] = ConfigLib.Get("wallet_currency");
            list[4] = org_money;
            list[5] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 1;
            record.subtype = 1;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 49;
            record.affect = change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 12	提现成功
        /// </summary>
        public static bool WithdrawPass(int member, decimal change, string id, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結，檢查錢包帳戶金額是否足夠支付費用
            if (CheckFreeMoney(wallet, change) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, -change);

            // 3. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 4. 累積錢包的 total_withdraw 金額
            WalletService.TotalWithdrawChange(member, change);

            // 4. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, id);

            // 5. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 6. 写入到 钱包纪录
            // 申請單號|金额|币别|原金额|原币别
            object[]? list = new object[5];
            list[0] = id;
            list[1] = change;
            list[2] = ConfigLib.Get("wallet_currency");
            list[3] = org_money;
            list[4] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 12;
            record.subtype = 1;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 12;
            record.affect = -change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 13	提现失败
        /// </summary>
        public static bool WithdrawFail(int member, decimal change, string id)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, change);

            // 3. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, id);

            return true;
        }

        /// <summary>
        /// 14	撤销提现
        /// </summary>
        public static bool WithdrawCanel(int member, decimal change, string id)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 3. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, id);

            return true;
        }

        /// <summary>
        /// 15	提现退回
        /// </summary>
        public static bool WithdrawBack(int member, decimal change, string id)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 3. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, id);

            return true;
        }

        /// <summary>
        /// 21	新手任务獎勵
        /// </summary>
        public static bool RewardNoviceQuests(int member, decimal change)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 因為是增加所以不用檢查錢包帳戶金額，但必須檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 gmoney 贈送金
            WalletService.GmoneyMoneyChange(member, change);

            // 交易帳號|獎勵金额
            object[]? list = new object[2];
            list[0] = member;
            list[1] = change;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 21;
            record.subtype = 1;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 21;
            record.affect = change;
            record.balance = wallet.balance;
            record.coupon = change;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);
            return true;
        }

        /// <summary>
        /// 23	活動管理費獎勵
        /// </summary>
        public static bool PromotRewardCoupon(int member, decimal change, string id, string account, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 因為是增加所以不用檢查錢包帳戶金額，但必須檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 coupon 贈送金
            WalletService.couponMoneyChange(member, change);

            // 3. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 4. 写入到 钱包纪录
            // 申請單號|交易帳號|獎勵金额|幣別|約當主錢包金額|主錢包幣別
            object[]? list = new object[6];
            list[0] = id;
            list[1] = account;
            list[2] = change;
            list[3] = ConfigLib.Get("wallet_currency");
            list[4] = org_money;
            list[5] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 23;
            record.subtype = 1;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 23;
            record.affect = change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 25	推广返佣入帳
        /// </summary>
        public static bool DirectSellingPass(int member, decimal change, string account, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 因為是增加所以不用檢查錢包帳戶金額，但必須檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, change);

            // 3. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 4. 写入到 钱包纪录
            // 交易帳號|入帳金额|幣別|約當主錢包金額|主錢包幣別
            object[]? list = new object[5];
            list[0] = account;
            list[1] = change;
            list[2] = ConfigLib.Get("wallet_currency");
            list[3] = org_money;
            list[4] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 25;
            record.subtype = 1;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 25;
            record.affect = change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 32	新合约审核通过
        /// </summary>
        public static bool NewBorrowPass(int member, decimal change, string id, string account, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結，檢查錢包帳戶金額是否足夠支付費用
            if (CheckFreeMoney(wallet, change) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, -change);

            // 3. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 4. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, $"b_{id}");

            // 5. 回寫錢包的 margin 保证金总额
            WalletService.MarginMoneyChange(member, change);

            // 6. 回寫錢包的 operate_balance 操盘总额
            WalletService.OperateMoneyChange(member, change);

            // 7. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 8. 写入到 钱包纪录
            // 申請單號|交易帳號|转入金額|幣別|約當主錢包金額|主錢包幣別
            object[]? list = new object[6];
            list[0] = id;
            list[1] = account;
            list[2] = change;
            list[3] = ConfigLib.Get("wallet_currency");
            list[4] = org_money;
            list[5] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 32;
            record.subtype = 2;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 32;
            record.affect = -change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 33	新合约审核不通过
        /// </summary>
        public static bool NewBorrowFail(int member, decimal change, string id)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 3. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, $"b_{id}");

            return true;
        }

        /// <summary>
        /// 34	新合约管理费
        /// </summary>
        public static bool BorrowManagementFee(int member, decimal change, string account, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結，檢查錢包帳戶金額是否足夠支付費用
            if (CheckFreeMoney(wallet, change) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, -change);

            // 3. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 4. 写入到 钱包纪录
            // 交易帳號|管理费金额|幣別|約當主錢包金額|主錢包幣別
            object[]? list = new object[5];
            list[0] = account;
            list[1] = change;
            list[2] = ConfigLib.Get("wallet_currency");
            list[3] = org_money;
            list[4] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 34;
            record.subtype = 2;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 34;
            record.affect = -change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 35	續期管理費自动扣款
        /// </summary>
        public static bool RenewalManagementFee(int member, decimal change, string account)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結，檢查錢包帳戶金額是否足夠支付費用
            if (CheckFreeMoney(wallet, change) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, -change);

            // 3. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 4. 写入到 钱包纪录
            // 交易帳號|管理费金额|幣別
            object[]? list = new object[3];
            list[0] = account;
            list[1] = change;
            list[2] = ConfigLib.Get("wallet_currency");

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 35;
            record.subtype = 2;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 35;
            record.affect = -change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 36	扣递延费
        /// </summary>
        public static bool DeferredFee(int member, decimal change, string account, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結，檢查錢包帳戶金額是否足夠支付費用
            if (CheckFreeMoney(wallet, change) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, -change);

            // 3. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 4. 写入到 钱包纪录
            // 交易帳號|扣款金额|幣別|約當主錢包金額|主錢包幣別
            object[]? list = new object[5];
            list[0] = account;
            list[1] = change;
            list[2] = ConfigLib.Get("wallet_currency");
            list[3] = org_money;
            list[4] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 36;
            record.subtype = 2;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 36;
            record.affect = -change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 38	追加保证金通過
        /// </summary>
        public static bool MaginCallPass(int member, decimal change, string id, string account, decimal org_money, string org_currency)
        {

            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結，檢查錢包帳戶金額是否足夠支付費用
            if (CheckFreeMoney(wallet, change) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, -change);

            // 3. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 4. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, $"bam_{id}");

            // 5. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 6. 写入到 钱包纪录
            // 申請單號|交易帳號|約當主錢包金額|主錢包幣別|转入金額|幣別
            object[]? list = new object[6];
            list[0] = id;
            list[1] = account;
            list[2] = change;
            list[3] = ConfigLib.Get("wallet_currency");
            list[4] = org_money;
            list[5] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 38;
            record.subtype = 2;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 38;
            record.affect = -change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 39	返还保证金不通過
        /// </summary>
        public static bool MaginCallFail(int member, decimal change, string id)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 3. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, $"bam_{id}");

            return true;

        }

        /// <summary>
        /// 40	配资结算(余额转出)
        /// </summary>
        public static bool BorrowSettle(int member, decimal change, string account, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, change);

            // 3. 回寫錢包的 margin 保证金总额
            WalletService.MarginMoneyChange(member, -change);

            // 4. 回寫錢包的 operate_balance 操盘总额
            WalletService.OperateMoneyChange(member, -change);

            // 5. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 6. 写入到 钱包纪录
            // 交易帳號|转入金額|幣別|交易号金額|交易号幣別
            object[]? list = new object[5];
            list[0] = account;
            list[1] = change;
            list[2] = ConfigLib.Get("wallet_currency");
            list[3] = org_money;
            list[4] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 40;
            record.subtype = 2;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 40;
            record.affect = change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 42	配资续期审核通过
        /// </summary>
        public static bool BorrowRenewalPass(int member, decimal change, string id, string account)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結，檢查錢包帳戶金額是否足夠支付費用
            if (CheckFreeMoney(wallet, change) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, -change);

            // 3. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 4. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, $"br_{id}");

            // 5. 重写取回钱包的金额
            wallet = WalletService.Find(member);

			// 6. 写入到 钱包纪录
			// 申請單號|交易帳號|转出金額|幣別
			object[]? list = new object[4];
            list[0] = id;
            list[1] = account;
            list[2] = change;
            list[3] = ConfigLib.Get("wallet_currency");

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 42;
            record.subtype = 2;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 42;
            record.affect = -change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 43	配资续期审核未通过
        /// </summary>
        public static bool BorrowRenewalFail(int member, decimal change, string id)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 3. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, $"br_{id}");

            return true;
        }

        /// <summary>
        /// 45	扩大配资审核通过
        /// </summary>
        /// <param name="member"></param>
        /// <param name="change">變動金額(主錢包)</param>
        /// <param name="id"></param>
        /// <param name="account">交易帳號</param>
        /// <param name="org_money">轉換後幣別(非主錢包)</param>
        /// <param name="org_currency">轉換幣別(非主錢包)</param>
        /// <returns></returns>
        public static bool ExpandBorrowPass(int member, decimal change, string id, string account, decimal org_money, string org_currency)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結，檢查錢包帳戶金額是否足夠支付費用
            if (CheckFreeMoney(wallet, change) == false) return false;

            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, -change);

            // 3. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 4. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, $"baf_{id}");

            // 5. 回寫錢包的 margin 保证金总额
            WalletService.MarginMoneyChange(member, change);

            // 6. 回寫錢包的 operate_balance 操盘总额
            WalletService.OperateMoneyChange(member, change);

            // 7. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 8. 写入到 钱包纪录
            // 申請單號|交易帳號|转出金額|幣別|交易号金額|交易号幣別
            object[]? list = new object[6];
            list[0] = id;
            list[1] = account;
            list[2] = change;
            list[3] = ConfigLib.Get("wallet_currency");
            list[4] = org_money;
            list[5] = org_currency;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 45;
            record.subtype = 2;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 45;
            record.affect = -change;
            record.balance = wallet.balance;
            record.coupon = 0;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 46	扩大配资审核未通过
        /// </summary>
        public static bool ExpandBorrowFail(int member, decimal change, string id)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 檢查錢包是否被凍結
            if (CheckFreeMoney(wallet, 0) == false) return false;

            // 2. 回寫錢包的 freeze 凍結金額
            WalletService.FreezeMoneyChange(member, -change);

            // 3. 刪除凍結紀錄
            WalletFreezeService.DeleteBySN(member, $"baf_{id}");

            return true;
        }

        /// <summary>
        /// 98	後台操作
        /// balance、coupon 金額直接帶入正負值
        /// </summary>
        public static bool AdminOperate(int member, decimal balance, decimal coupon, string reason)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 因為是增加所以不用檢查錢包帳戶金額，但必須檢查錢包是否被凍結
            if (balance > 0)
            {
                if (CheckFreeMoney(wallet, 0) == false) return false;
            }//因為是減少檢查錢包帳戶金額，檢查錢包是否被凍結
            else
            {
                if (CheckFreeMoney(wallet, -balance) == false) return false;
            }
            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, balance);

            // 3. 回寫錢包的 coupon 贈送金
            WalletService.couponMoneyChange(member, coupon);

            // 4. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 5. 写入到 钱包纪录
            // 金额|操作说明
            object[]? list = new object[2];
            list[0] = balance;
            list[1] = reason;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 98;
            record.subtype = 1;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 98;
            record.affect = balance;
            record.balance = wallet.balance;
            record.coupon = coupon;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 99	帳戶金額調整
        /// balance、coupon 金額直接帶入正負值
        /// </summary>
        public static bool ErrorCorrection(int member, decimal balance, decimal coupon, string reason)
        {
            // 1. sql 取出 member 钱包做检核,
            WalletDto wallet = WalletService.Find(member);
            // 因為是增加所以不用檢查錢包帳戶金額，但必須檢查錢包是否被凍結
            if (balance > 0)
            {
                if (CheckFreeMoney(wallet, 0) == false) return false;
            }//因為是減少檢查錢包帳戶金額，檢查錢包是否被凍結
            else
            {
                if (CheckFreeMoney(wallet, -balance) == false) return false;
            }
            // 2. 回寫錢包的 balance 账户金额
            WalletService.BalanceMoneyChange(member, balance);

            // 3. 回寫錢包的 coupon 贈送金
            WalletService.couponMoneyChange(member, coupon);

            // 4. 重写取回钱包的金额
            wallet = WalletService.Find(member);

            // 5. 写入到 钱包纪录
            // 金额|調整说明
            object[]? list = new object[2];
            list[0] = balance;
            list[1] = reason;

            WalletRecordRequest record = new WalletRecordRequest();
            record.member_pk = member;
            record.type = 99;
            record.subtype = 1;
            record.currency = ConfigLib.Get("wallet_currency");
            record.temp_id = 99;
            record.affect = balance;
            record.balance = wallet.balance;
            record.coupon = coupon;
            record.createtime = DateTime.UtcNow;
            record.list = list;
            WalletRecordLib.Save(record);

            return true;
        }

        /// <summary>
        /// 确认钱包的钱是否足够
        /// </summary>
        public static bool IsRequestrMoneyOk(int member, decimal change)
        {
            WalletDto wallet = WalletService.Find(member);
            if (wallet.status == false) return false;
            if (wallet.balance >= change) return true;
            return false;
        }

        /// <summary>
        /// 判断钱包余额是否足够
        /// </summary>
        private static bool CheckFreeMoney(WalletDto wallet, decimal change)
        {
            if (wallet.status == false) return false;
            if (wallet.balance >= change) return true;
            return false;
        }

        /// <summary>
        /// 寫入冻结資料表
        /// </summary>
        private static void FreezeMoney(int member, decimal change, string id, int subtype)
        {
            var walletfreeze = new WalletFreezeDto
            {
                member_fk = member,
                sn = id,
                freeze = change,
                subtype = subtype,
                create_time = DateTime.UtcNow
            };
            WalletFreezeService.Insert(walletfreeze);

        }

        /// <summary>
        /// 26	抵用折扣券
        /// </summary>
        public static bool UseCoupon(int member_id, decimal amount, string reason)
        {
            WalletDto wallet = WalletService.Find(member_id);
            //檢查折扣券是否足夠
            if (CheckFreeCoupon(wallet, amount) == false) return false;

            //扣除折扣券
            WalletService.couponMoneyChange(member_id, -amount);

            object[]? list = new object[2];
            list[0] = amount;
            list[1] = reason;

            //写入钱包纪录
            WalletRecordRequest record = new WalletRecordRequest
            {
                member_pk = member_id,
                type = 26,
                subtype = 1,
                currency = ConfigLib.Get("wallet_currency"),
                temp_id = 26,
                affect = -amount,
                balance = wallet.balance,
                coupon = wallet.coupon - amount,
                createtime = DateTime.UtcNow,
                list = list
            };
            WalletRecordLib.Save(record);
            return true;
        }

        /// <summary>
        /// 确认钱包的折扣券余额是否足够
        /// </summary>
        public static bool IsRequestCouponOk(int member, decimal amount)
        {
            var wallet = WalletService.Find(member);
            if (wallet == null)
                return false;
            else
                return CheckFreeCoupon(wallet, amount);
        }

        /// <summary>
        /// 判断钱包折扣券余额是否足够
        /// </summary>
        private static bool CheckFreeCoupon(WalletDto wallet, decimal amount)
        {
            if (wallet.status)
                return wallet.coupon >= amount;
            else
                return false;
        }
    }
}
