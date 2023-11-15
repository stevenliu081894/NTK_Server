using DB.Services;
using Models.Dto;
using NTKServer.Models.Wallet;

namespace NTKServer.Libs
{
    public class WalletRecordLib
    {
        public static void Save(WalletRecordRequest req)
        {
            MemberDto memberDto = MemberService.Find(req.member_pk);

            WalletTemplateDto walletTemplateDto = WalletTemplateService.GetByTempId(req.temp_id, memberDto.lang);

            // 取出模板
            var info = walletTemplateDto?.template;

            string mparams = string.Empty;
            if (req.list != null)
            {
                // 資料套入模板
                for (int i = 0; i < req.list.Length; i++)
                {
                    info = info.Replace($"#{i}#", req.list[i].ToString());
                    mparams = mparams + req.list[i].ToString() + "|";
                }
            }

            // 寫到 wallet_record
            WalletRecordService.FindPkAfterInsert(new WalletRecordDto()
            {
                member_fk = req.member_pk,
                type = req.type,
                subtype = req.subtype,
                currency = req.currency,
                affect = req.affect,
                balance = req.balance,
                coupon = req.coupon,
                param = mparams,
                templat_id = req.temp_id,
                info = info,
                create_time = req.createtime,
                create_ip = ""
            });

        }
    }
}
