using System.Text.Json.Serialization;

namespace Models
{
    public class UploadImagesResponse
    {
        [JsonPropertyName("server_url")]
        public string server_url { get; set; } // 图片所在的伺服器位置
        [JsonPropertyName("img_urls")]
        public string[] img_urls { get; set; } // 图片于伺服器相对路径
    }
}
