using DB.Services;
using Models.Dto;
using Org.BouncyCastle.Ocsp;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Business;
using NTKServer.Models;
using NTKServer.Models.Dto;
using NTKServer.Models.Wallet;

namespace NTKServer.Libs
{
    public class MemberTaskLib
    {
        public MemberTaskLib()
        {

        }

        /// <summary>
        /// 新手任務完成
        /// </summary>
        public static bool MemberTaskFinish(int member_pk, int sub_type, string lang)
        {
            bool re = false;
            string currency = ConfigLib.Get("wallet_currency");
            if (currency == "") return false;

            MemberTaskDto memberTaskDto = MemberTaskService.FindBySubType_Currency_Lang(sub_type, lang, currency);
            if (memberTaskDto == null || memberTaskDto.currency.ToLower() != currency.ToLower()) return false;

            MemberTaskDto memberTaskDtoCN = MemberTaskService.FindBySubType_Currency_Lang(sub_type, "CN", currency);

            int type = 1;
            // 檢查是否完成過
            WalletCouponRecordDto walletCouponRecDto = WalletCouponRecordService.FindByMemberFK_Type_SubType(member_pk, type, sub_type);
            if (walletCouponRecDto != null) return false;

            WalletDto wallet = WalletService.Find(member_pk);
            if (wallet == null) return false;
            if (wallet.currency.ToLower() != currency.ToLower()) return false;

            WalletTemplateDto walletTemplateDto = WalletTemplateService.GetByTempId(23, "CN");
            string info = walletTemplateDto.template
    .Replace("#0#", memberTaskDtoCN.title)
    .Replace("#1#", memberTaskDto.coin.ToString());

            WalletCouponRecordDto walletCouponRecordDto = new WalletCouponRecordDto
            {
                member_fk = member_pk,
                currency = wallet.currency,
                affect = memberTaskDto.coin,
                coupon_before = wallet.balance,
                coupon_after = wallet.balance,
                type = type,
                sub_type = sub_type,
                info = info,
                create_time = DateTime.UtcNow
            };
            int pk = WalletCouponRecordService.FindPkAfterInsert(walletCouponRecordDto);
            re = NTKServer.Libs.WalletLib.RewardNoviceQuests(member_pk, memberTaskDto.coin);
            return re;
        }

        
    }
}
