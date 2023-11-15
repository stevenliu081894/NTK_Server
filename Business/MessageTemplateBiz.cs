using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.MessageTemplate;

namespace NTKServer.Business
{
    public class MessageTemplateBiz
    {
        #region CRUD
		public static List<MessageTemplateList> GetMessageTemplateList(MessageTemplateFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return MessageTemplateService.FindMessageTemplateList(whereSql);
        }

        public static MessageTemplateDto Get(int pk)
        {
            return MessageTemplateService.Find(pk);
        }

        public static void PostCreate(MessageTemplateDto req)
        {
            if (MessageTemplateService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(MessageTemplateDto req)
        {
            if( MessageTemplateService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            MessageTemplateService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
