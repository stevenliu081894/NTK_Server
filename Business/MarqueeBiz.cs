using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Tool;
using NTKServer.ViewModels.Marquee;

namespace NTKServer.Business
{
    public class MarqueeBiz
    {
        #region CRUD
		public static List<MarqueeList> GetMarqueeList(MarqueeFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsMarqService.FindMarqueeList(whereSql);
        }

        public static CmsMarqDto Get(int pk)
        {
            return CmsMarqService.Find(pk);
        }

        public static void PostCreate(CmsMarqDto req)
        {
            if (CmsMarqService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static void PostEdit(CmsMarqDto req)
        {
            if( CmsMarqService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            CmsMarqService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
