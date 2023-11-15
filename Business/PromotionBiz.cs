using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.Promotion;

namespace NTKServer.Business
{
    public class PromotionBiz
    {
        #region CRUD
		public static List<PromotionList> GetPromotionList(PromotionFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsPromotionService.FindPromotionList(whereSql)?
                .Select(messageRecord => PublicTool.convertUtcToLocalTime(messageRecord)).ToList();
        }

        public static CmsPromotionDto Get(int pk)
        {
            return CmsPromotionService.Find(pk);
        }

        public static async void PostCreate(CmsPromotionDto req)
        {

            if (req.img_file != null) {
                var image_url = await UploadImageLib.UploadImage(FileManagementLib.Folder.article, req.img_file);
                req.img_url = image_url;
            }
            if (CmsPromotionService.FindPkAfterInsert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static async void PostEdit(CmsPromotionDto req)
        {
            if (req.img_file != null) {
                var image_url = await UploadImageLib.UploadImage(FileManagementLib.Folder.article, req.img_file);
                req.img_url = image_url;
            }
            if( CmsPromotionService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(int pk)
        {
            CmsPromotionService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
