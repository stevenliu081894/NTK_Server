using Models;
using RestSharp;
using NTKServer.Internal;


namespace NTKServer.Libs
{
    public class FileManagementLib
    {
        public enum Folder {
            flag = 1, // 國旗
            id = 2, // 身分證
            banner = 3, // 輪播圖
            article = 4, // 文章
            message = 5, // 站內信
            bank_book = 6, // 存摺
        }

        public static async Task<GetFilesResponse> GetFilesInDirectory(Folder dir_name)
        {
            try
            {
                var host = ConfigLib.Get("fileserver");
                var client = new RestClient($"{host}/api/");
                var request = new RestRequest("ApiFile/getfilesindirectory");
                request.AddParameter("dir_name", dir_name.ToString());
                    
                var response = await client.PostAsync<APIResponse<GetFilesResponse>>(request);
                if (response != null)
                    return response.data;

                throw new AppException(response.status, response.message);
            }
            catch (Exception)
            {
                throw new AppException(2100, "upload_service_exception");
            }
        }

        public static async Task<GetFilesResponse> GetAllFiles()
        {
            try
            {
                var host = ConfigLib.Get("fileserver");
                var client = new RestClient($"{host}/api/");
                var request = new RestRequest("ApiFile/getallfiles");
                    
                var response = await client.PostAsync<APIResponse<GetFilesResponse>>(request);
                if (response != null)
                    return response.data;

                throw new AppException(response.status, response.message);
            }
            catch (Exception)
            {
                throw new AppException(2100, "upload_service_exception");
            }
        }

        public static async Task CreateDirectory(Folder dir_name)
        {
            try
            {
                var host = ConfigLib.Get("fileserver");
                var client = new RestClient($"{host}/api/");
                var request = new RestRequest("ApiFile/createdirectory");
                request.AddParameter("dir_name", dir_name.ToString());
                    
                var response = await client.PostAsync<APIResponse>(request);
                if (response?.status != 200)
                    throw new AppException(response.status, response.message);
            }
            catch (Exception)
            {
                throw new AppException(2100, "upload_service_exception");
            }
        }

        public static async Task DeleteFile(Folder dir_name, string file_name)
        {
            try
            {
                var host = ConfigLib.Get("fileserver");
                var client = new RestClient($"{host}/api/");
                var request = new RestRequest("ApiFile/deletefile");
                request.AddParameter("dir_name", dir_name.ToString());
                request.AddParameter("file_name", file_name);
                    
                var response = await client.PostAsync<APIResponse>(request);
                if (response?.status != 200)
                    throw new AppException(response.status, response.message);
            }
            catch (Exception)
            {
                throw new AppException(2100, "upload_service_exception");
            }
        }
    }
}
