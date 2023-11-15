using DB.Services;
using Models.Dto;

namespace NTKServer.Libs
{
    /// <summary>
    /// 寄送站内信
    /// </summary>
    public class SendMessageLib
    {
        public static void Send(int member_pk, string title, string info, int classify)
        {
            MemberDto member = MemberService.Find(member_pk);

            var create_time = DateTime.UtcNow;
            // 寫到 message_record
            MessageRecordService.FindPkAfterInsert(new MessageRecordDto()
            {
                isbatch = false,
                receiver_table = AccountTypeEnum.Member,
                receiver_fk = member_pk,
                sender_table = AccountTypeEnum.AdminUser,
                sender_fk = Convert.ToInt32(member.admin_user_fk),
                title = title,
                info = info,
                read_status = MessageReadStatusEnum.Unread,
                send_status = MessageSendStatus.Sented,
                send_type = MessageTransTypeEnum.Internal,  // 站內信
                create_time = create_time,
                read_time = null,
                sent_time = create_time,
                classify = classify
            });
        }

        public static void Send(int member_pk, int temp_id, params object[] list)
        {
            MemberDto member = MemberService.Find(member_pk);
            var template = MessageTemplateService.FindByTemplateId(temp_id, member.lang);

            // 取出模板
            var info = (template.template).ToString();

            // 資料套入模板
            for (int i = 0; i < list.Length; i++)
            {
                info = info.Replace($"#{i}#", list[i].ToString());
            }

            // 寫到 message_record
            Send(member_pk, template.title, info, 1);
        }
    }
}
