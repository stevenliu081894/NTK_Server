using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.Member;

namespace NTKServer.Business
{
    public class MemberBiz
    {
        #region CRUD
		public static List<MemberList> GetMemberList(MemberFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return MemberService.FindMemberList(whereSql)?
                .Select(member => PublicTool.convertUtcToLocalTime(member)).ToList();
        }

        public static MemberDto Get(int pk)
        {
            return MemberService.Find(pk);
        }

        public static void PostCreate(MemberDto req)
        {
            if (MemberService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(MemberDto req)
        {
            if( MemberService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            MemberService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
