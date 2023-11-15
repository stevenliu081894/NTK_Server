using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.Banner;

namespace NTKServer.Business
{
    public class BannerBiz
    {
        #region CRUD
		public static List<BannerList> GetBannerList(BannerFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return CmsBannerService.FindBannerList(whereSql);
        }

        public static CmsBannerDto Get(int cms_files_fk)
        {
            return CmsBannerService.Find(cms_files_fk);
        }

        public static async void PostCreate(CmsBannerDto req)
        {
            try {
                req.url = ConfigLib.Get("filesite");
                var pks = await UploadImageLib.UploadImage(FileManagementLib.Folder.banner, "cms_banner", 0, false, req.image);
                req.cms_files_fk = pks[0];
                var cms_banner_pk = CmsBannerService.FindPkAfterInsert(req);

                CmsFilesService.UpdateKey(pks[0], cms_banner_pk);
            }
            catch (Exception) {
                throw new AppException("資料建立失敗");
            }
        }

        public static async void PostEdit(CmsBannerDto req)
        {
            req.url = ConfigLib.Get("filesite");
            await UploadImageLib.UpdateUploadImage(FileManagementLib.Folder.banner, "cms_banner", req.cms_files_fk, false, req.image);

            if ( CmsBannerService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static async void Delete(int cms_files_fk)
        {

            var file = CmsFilesService.Find(cms_files_fk);
            await UploadFileLib.DeleteFile(file.url);
            CmsBannerService.Remove(cms_files_fk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
