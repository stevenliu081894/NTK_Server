using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.MessageRecord;

namespace NTKServer.Business
{
    public class MessageRecordBiz
    {
        #region CRUD
		public static List<MessageRecordList> GetMessageRecordList(MessageRecordFilter? filter)
        {
            string whereSql = SqlTool.Build(filter).Must("classify = 2").Must("receiver_table = 2");
            return MessageRecordService.FindMessageRecordList(whereSql)?
                .Select(messageRecord => PublicTool.convertUtcToLocalTime(messageRecord)).ToList();
        }

        public static MessageRecordDto Get(int pk)
        {
            return MessageRecordService.Find(pk);
        }

        public static void PostCreate(MessageRecordDto req)
        {
            try
            {
                SendMessageLib.Send(req.receiver_fk, req.title, req.info, 2);
            }
            catch (Exception)
            {
                throw new AppException("傳送站內信失敗");
            }
        }

        public static void PostEdit(MessageRecordDto req)
        {
            if( MessageRecordService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            MessageRecordService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		

        public static MessageRecordEditVm GetEditVm(int pk)
        {
            return MessageRecordService.FindMessageRecordEditVm(pk);
        }

		public static MessageRecordEditVm GetAppendVm(int member)
		{
            var usr = MemberService.Find(member);
            MessageRecordEditVm vm = new()
            {
                receiver_fk = usr.pk,
                account = usr.account
            };
            return vm;
		}

		#endregion
	}
}
