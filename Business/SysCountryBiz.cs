using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;
using NTKServer.Tool;
using NTKServer.ViewModels.SysCountry;

namespace NTKServer.Business
{
    public class SysCountryBiz
    {
        #region CRUD
		public static List<SysCountryList> GetSysCountryList(SysCountryFilter? filter)
        {
            string whereSql = SqlTool.Build(filter);
            return SysCountryService.FindSysCountryList(whereSql);
        }

        public static SysCountryDto Get(string pk)
        {
            return SysCountryService.Find(pk);
        }

        public static async void PostCreate(SysCountryDto req)
        {
            var image_url = await UploadImageLib.UploadImage(FileManagementLib.Folder.flag, req.flag_file);
            req.flag = image_url;
            if (SysCountryService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static async void PostEdit(SysCountryDto req)
        {
            if (req.flag_file != null) {
                var image_url = await UploadImageLib.UploadImage(FileManagementLib.Folder.flag, req.flag_file);
                req.flag = image_url;
            }
            if( SysCountryService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(string pk)
        {
            SysCountryService.Remove(pk);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
