using DB.Services;
using Models.Dto;

namespace NTKServer.Libs
{
    public class TradeRecordLib
    {
        public static void Save(TradeMoneyRecoreRequest request)
        {
            var member = MemberService.Find(request.member_fk);
            var tradeTemplate = TradeTemplateService.GetByTempId(request.temp_id, member.lang);

            // 取出模板
            var info = tradeTemplate?.template ?? "";

            // 資料套入模板
            for (int i = 0; i < request.list.Length; i++)
            {
                info = info.Replace($"#{i}#", Convert.ToString(request.list[i]));
            }

            // 寫到 trade_money_record
            TradeMoneyRecordService.Insert(new TradeMoneyRecordDto()
            {
                member_fk = request.member_fk,
                sub_account = request.sub_account,
                sn = request.sn,
                temp_id = request.temp_id,
                currency = request.currency,
                affect = request.affect,
                exchange = request.exchange,
                wallet_amount = request.wallet_amount,
                info = info,
                reviewer = request.reviewer,
                create_datetime = request.create_datetime
            });
        }

        public static void Save(TradeStockRecoreRequest request)
        {
            var member = MemberService.Find(request.member_fk);
            var tradeTemplate = TradeTemplateService.GetByTempId(request.temp_id, member.lang);

            // 取出模板
            var info = tradeTemplate?.template ?? "";

            // 資料套入模板
            for (int i = 0; i < request.list.Length; i++)
            {
                info = info.Replace($"#{i}#", Convert.ToString(request.list[i]));
            }

            // 寫到 trade_money_record
            TradeMoneyRecordService.Insert(new TradeMoneyRecordDto()
            {
                member_fk = request.member_fk,
                sub_account = request.sub_account,
                sn = request.sn,
                temp_id = request.temp_id,
                currency = request.currency,
                affect = request.affect,
                reviewer = request.reviewer,
                market = request.market,
                stock_code = request.stock_code,
                stock_name = request.stock_name,
                trade_deal_fk = request.trade_deal_fk,
                info = info,
                create_datetime = request.create_datetime
            });
        }
    }
}
