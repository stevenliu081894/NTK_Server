namespace NTKServer.Models
{
    public class AdminSession
    {
        public int pk { get; set; }
        public int role { get; set; }
        public string account { get; set; }
        public string nickName { get; set; }
        public int avatar { get; set; }
        public string lang { get; set; }
        public bool change_password { get; set; }
    }
}
