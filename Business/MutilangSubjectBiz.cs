using DB.Services;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;

namespace NTKServer.Business
{
    public class MutilangSubjectBiz
    {
        #region CRUD
		public static List<MutilangSubjectDto> GetMutilangSubjectList()
        {

            return MutilangSubjectService.FindAll();
        }

        public static MutilangSubjectDto Get(string lang)
        {
            return MutilangSubjectService.Find(lang);
        }

        public static async void PostCreate(MutilangSubjectDto req)
        {
            var url = await UploadImageLib.UploadImage(FileManagementLib.Folder.flag, req.icon_file);
            req.icon = url;
            if (MutilangSubjectService.Insert(req) == 0)
            {
                throw new AppException(3020, "insert_record_false");
            }
        }

        public static async void PostEdit(MutilangSubjectDto req)
        {
            if (req.icon_file != null) {
                var url = await UploadImageLib.UploadImage(FileManagementLib.Folder.flag, req.icon_file);
                req.icon = url;
            }
            if( MutilangSubjectService.UpdateFull(req) == 0)
            {
                throw new AppException(3010, "record_update_false");
            }
        }

        public static void Delete(string lang)
        {
            MutilangSubjectService.Remove(lang);
        }

        #endregion
		
		#region ViewModel		


		#endregion		
	}
}
