using DB.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using NTKServer.Internal;
using NTKServer.Libs;

namespace NTKServer.Business
{
    public class UploadBiz
    {
        public static async Task<string> UploadImage([FromForm]IFormFile image, FileManagementLib.Folder folder)
        {
            try {
                var host = ConfigLib.Get("filesite");
                var url = await UploadImageLib.UploadImage(folder, image);

                return host + url;
            }
            catch (Exception) {
                throw new AppException("資料建立失敗");
            }
        }
	}
}
