using Models;
using RestSharp;
using NTKServer.Models;
using NTKServer.Libs;
using DB.Services;
using Models.Dto;
using NTKServer.Internal;


namespace NTKServer.Libs
{
    public class UploadImageLib
    {
        /// <summary>
        /// 範例：上傳客服圖片
        /// UploadImages(Folder.article, "cms_support", 1, false, 照片的form file)
        /// </summary>
        /// <param name="img_folder"></param>
        /// <param name="table_name"></param>
        /// <param name="pk"></param>
        /// <param name="change_name"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public static async Task<List<int>> UploadImages(FileManagementLib.Folder img_folder, string table_name, int pk, bool change_name, List<IFormFile> images)
        {
            try
            {
                var host = ConfigLib.Get("fileserver");
                var client = new RestClient($"{host}/api/");
                var request = new RestRequest("ApiImg/uploadimages");
                request.AddParameter("img_folder", img_folder.ToString());
                request.AddParameter("change_name", change_name);
                List<int> pks = new List<int> {};
                    
                foreach (var image in images) {
                    var ms = new MemoryStream();
                    image.CopyTo(ms);
                    request.AddFile("images", ms.ToArray(), image.FileName, image.ContentType);
                }

                var response = await client.PostAsync<APIResponse<UploadImagesResponse>>(request);
                if (response != null) {
                    foreach (var img_url in response.data.img_urls) {
                        pks.Add(CmsFilesService.FindPkAfterInsert(new CmsFilesDto {url = img_url, file_type = 1, table = table_name, key = pk}));
                    }
                }

                return pks;
            }
            catch (Exception ex)
            {
                throw new AppException(2100, "upload_service_exception");
            }
        }
        
        public static async Task<List<int>> UploadImage(FileManagementLib.Folder img_folder, string table_name, int pk, bool change_name, IFormFile image) 
        {
            List<IFormFile> image_list = new List<IFormFile> {image};
            return await UploadImages(img_folder, table_name, pk, change_name, image_list);
        }

		public static async Task UpdateUploadImage(FileManagementLib.Folder img_folder, string table_name, int pk, bool change_name, IFormFile image)
		{
			List<IFormFile> image_list = new List<IFormFile> { image };
			await UpdateUploadImages(img_folder, table_name, pk, change_name, image_list);
		}

		/// <summary>
		/// 範例：上傳客服圖片
		/// UploadImages(Folder.article, "cms_support", 1, false, 照片的form file)
		/// </summary>
		/// <param name="img_folder"></param>
		/// <param name="table_name"></param>
		/// <param name="pk"></param>
		/// <param name="change_name"></param>
		/// <param name="images"></param>
		/// <returns></returns>
		/// <exception cref="AppException"></exception>
		public static async Task UpdateUploadImages(FileManagementLib.Folder img_folder, string table_name, int pk, bool change_name, List<IFormFile> images)
		{
			try
			{
				var host = ConfigLib.Get("fileserver");
				var client = new RestClient($"{host}/api/");
				var request = new RestRequest("ApiImg/uploadimages");
				request.AddParameter("img_folder", img_folder.ToString());
				request.AddParameter("change_name", change_name);
				List<int> pks = new List<int> { };

				foreach (var image in images)
				{
					var ms = new MemoryStream();
					image.CopyTo(ms);
					request.AddFile("images", ms.ToArray(), image.FileName, image.ContentType);
				}

				var response = await client.PostAsync<APIResponse<UploadImagesResponse>>(request);
				if (response != null)
				{
					foreach (var img_url in response.data.img_urls)
					{
                        CmsFilesService.Update(new CmsFilesDto { url = img_url, file_type = 1, table = table_name, key = pk });
					}
				}
			}
			catch (Exception ex)
			{
				throw new AppException(2100, "upload_service_exception");
			}
		}


		public static async Task<string> UploadImage(FileManagementLib.Folder img_folder, IFormFile image)
        {
            try
            {
                var host = ConfigLib.Get("fileserver");
                var client = new RestClient($"{host}/api/");
                var request = new RestRequest("ApiImg/uploadimages");
                request.AddParameter("img_folder", img_folder.ToString());
                request.AddParameter("change_name", true);
                List<int> pks = new List<int> {};
                    
                var ms = new MemoryStream();
                image.CopyTo(ms);
                request.AddFile("images", ms.ToArray(), image.FileName, image.ContentType);

                var response = await client.PostAsync<APIResponse<UploadImagesResponse>>(request);
                if (response == null) {
                    throw new AppException(2100, "upload_service_exception");
                }

                foreach (var img_url in response.data.img_urls) {
                    CmsFilesService.FindPkAfterInsert(new CmsFilesDto {url = img_url, file_type = 1, table = "", key = 0});
                }

                return response.data.img_urls[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new AppException(2100, "upload_service_exception");
            }
        }
        
        public static string RemoveHostName(string content)
        {
            try
            {
                var host = ConfigLib.Get("filesite");
                return content.Replace(host, "#0#");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new AppException(2100, "upload_service_exception");
            }
        }

        public static string AddHostName(string content)
        {
            try
            {
                var host = ConfigLib.Get("filesite");
                return content.Replace("#0#", host);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new AppException(2100, "upload_service_exception");
            }
        }
    }
}
