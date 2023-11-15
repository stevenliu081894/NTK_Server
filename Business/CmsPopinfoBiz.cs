using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.CmsPopinfo;

namespace NTKServer.Business
{
    public class CmsPopinfoBiz
    {
        #region CRUD
		public static List<CmsPopinfoList> GetCmsPopinfoList(CmsPopinfoFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsPopinfoService.FindCmsPopinfoList(whereSql);
        }

        public static CmsPopinfoDto Get(int pk)
        {
            return CmsPopinfoService.Find(pk);
        }

        public static void PostCreate(CmsPopinfoDto req)
        {
            if (CmsPopinfoService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(CmsPopinfoDto req)
        {
            if( CmsPopinfoService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            CmsPopinfoService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
