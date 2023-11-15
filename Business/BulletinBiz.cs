using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.Bulletin;

namespace NTKServer.Business
{
    public class BulletinBiz
    {
        #region CRUD
		public static List<BulletinList> GetBulletinList(BulletinFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsBulletinService.FindBulletinList(whereSql)?
                .Select(bulletin => PublicTool.convertUtcToLocalTime(bulletin)).ToList();
        }

        public static CmsBulletinDto Get(int pk)
        {
            return CmsBulletinService.Find(pk);
        }

        public static void PostCreate(CmsBulletinDto req)
        {
            if (CmsBulletinService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(CmsBulletinDto req)
        {
            if( CmsBulletinService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            CmsBulletinService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
