using Dapper;
using Microsoft.DotNet.Scaffolding.Shared;
using Serilog.Events;
using Models.Dto;
using DB.Services;
using System.Collections.Generic;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace StockAdmin.Libs
{
    /*
    //public class WalletRecoreLib
    //{
    //    private readonly SendMessageLib _sendMessageLib;
    //    private readonly AdminActionService _adminActionService;

    //    public WalletRecoreLib(SendMessageLib sendMessageLib, AdminActionService adminActionService)
    //    {
    //        _sendMessageLib = sendMessageLib;
    //        _adminActionService = adminActionService;
    //    }
    //    public void Log(int member, int tempId, string lang, params object[] variables)
    //    {
    //        // 1. 取出模板
    //        // select template from  admin_action where temp_id = tempId and lang = lang
    //        // 舉例模板值為: 您的交易帳號 #0# 申请了追加保证金,追加金额 #1# 元
    //        //string template = "您的交易帳號 #0# 申请了追加保证金,追加金额 #1# 元";  // 這一行改成從 db 取得

    //        var actionDto = _adminActionService.Find(tempId, lang);
    //        string template = actionDto.remark;

    //        // 2. 套模板
    //        for (int i = 0; i < variables.Length; i++)
    //        {
    //            template.Replace($"#{i}#", variables[i].ToString());
    //        }

    //        // 3. 正式寫入站內信紀錄
    //        // 寄信人的取值方法: 由 member 取得客服人員 pk, 

    //    }
    //}
    */

    public class WalletRecoreLib
    {
        private readonly MemberService _memberService;
        private readonly WalletRecordService _walletRecordService;
        private readonly WalletTemplateRecordService _walletTemplateRecordService;

        public static WalletRecoreLib(IServiceProvider provider)
        {
            _memberService = provider.GetRequiredService<MemberService>();
            _walletRecordService = provider.GetRequiredService<WalletRecordService>();
            _walletTemplateRecordService = provider.GetRequiredService<WalletTemplateRecordService>();
        }

        public static void Log(int member_pk, int temp_id, params object[] list)
        {
            var member = _memberService.GetMemberDetail(member_pk);
            var wallet_template = _walletTemplateRecordService.GetByTempId(temp_id, member.Lang);
            var freezeEntity = _walletTemplateRecordService.GetFreeze(temp_id, member_pk);

            // 取出模板
            var info = (wallet_template?.Template).Text();

            // 資料套入模板
            for (int i = 0; i < list.Length; i++)
            {
                info = info.Replace($"#{i}#", list[i].Text());
            }

            // 寫到 message_record
            _walletRecordService.Insert(new WalletRecordDto()
            {
                MemberFk = member_pk,
                Type = 0, //TODO: 資金類型
                Subtype = freezeEntity.subtype,
                Currency = "",
                Affect = 1,
                Freeze = 1,
                Balance = 1,
                Coupon = 1,
                Param = "",
                TemplatId = temp_id,
                Info = info,
                CreateTime = DateTime.UtcNow,
                CreateIp = ""
            });
        }
    }
}
