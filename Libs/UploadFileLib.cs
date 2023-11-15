using Models;
using RestSharp;
using NTKServer.Models;
using NTKServer.Libs;
using DB.Services;
using Models.Dto;
using NTKServer.Internal;


namespace NTKServer.Libs
{
    public class UploadFileLib
    {
        public static async Task DeleteFile(FileManagementLib.Folder dir_name, string file_name)
        {
            try
            {
                var host = ConfigLib.Get("fileserver");
                var client = new RestClient($"{host}/api/");
                var request = new RestRequest("apifile/deletefile");
                request.AddParameter("dir_name", dir_name.ToString());
                request.AddParameter("file_name", file_name);
                    
                await client.PostAsync<APIResponse<UploadImagesResponse>>(request);
            
                return;
            }
            catch (Exception ex)
            {
                throw new AppException(2100, "upload_service_exception");
            }
        }

        public static async Task DeleteFile(string file_url)
        {
            try
            {
                var host = ConfigLib.Get("fileserver");
                var client = new RestClient(host);
                var request = new RestRequest("api/apifile/deletefile?file_name=" + file_url + "&dir_name=\"\"");

                await client.PostAsync<APIResponse>(request);
            
                return;
            }
            catch (Exception ex)
            {
                throw new AppException(2100, "upload_service_exception");
            }
        }
    }
}
