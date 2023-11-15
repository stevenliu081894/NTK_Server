using System.Text.Json.Serialization;

namespace Models
{
    public class UploadSuccess
    {
        public int uploaded { get; set; } 
        public string file_name { get; set; }
        public string url { get; set; }
    }
}
