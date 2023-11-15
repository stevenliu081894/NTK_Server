using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.CmsSupport;

namespace NTKServer.Business
{
    public class CmsSupportBiz
    {
        #region CRUD
		public static List<CmsSupportList> GetCmsSupportList(CmsSupportFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsSupportService.FindCmsSupportList(whereSql);
        }

        public static CmsSupportDto Get(int pk)
        {
            return CmsSupportService.Find(pk);
        }

        public static void PostCreate(CmsSupportDto req)
        {
            if (CmsSupportService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(CmsSupportDto req)
        {
            if( CmsSupportService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            CmsSupportService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
