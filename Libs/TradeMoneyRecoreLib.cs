using DB.Services;
using Models.Dto;

namespace NTKServer.Libs
{
    public class TradeMoneyRecoreLib
    {
        public static void Log(int member_pk, int temp_id, params object[] list)
        {
            var member = MemberService.Find(member_pk);
            var tradeTemplate = TradeTemplateService.GetByTempId(temp_id, member.lang);

            // 取出模板
            var info = tradeTemplate?.template ?? "";

            // 資料套入模板
            for (int i = 0; i < list.Length; i++)
            {
                info = info.Replace($"#{i}#", Convert.ToString(list[i]));
            }

            // 寫到 message_record
            TradeMoneyRecordService.Insert(new TradeMoneyRecordDto()
            {
                member_fk = member_pk,
                sub_account = "",
                trade_deal_fk = 1,
                sn = "",
                temp_id = temp_id,
                op = 1,
                currency = "",
                balance = 1,
                affect = 1,
                exchange = 1,
                wallet_amount = 1,
                info = info,
                reviewer = "",
                create_datetime = DateTime.UtcNow,
                market = "",
                stock_code = "",
                stock_name = ""
            });
        }
    }
}
