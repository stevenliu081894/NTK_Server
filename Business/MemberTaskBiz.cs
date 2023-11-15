using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.MemberTask;

namespace NTKServer.Business
{
    public class MemberTaskBiz
    {
        #region CRUD
		public static List<MemberTaskList> GetMemberTaskList(MemberTaskFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return MemberTaskService.FindMemberTaskList(whereSql);
        }

        public static MemberTaskDto Get(int pk)
        {
            return MemberTaskService.Find(pk);
        }

        public static void PostCreate(MemberTaskDto req)
        {
            if (MemberTaskService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(MemberTaskDto req)
        {
            if( MemberTaskService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            MemberTaskService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
