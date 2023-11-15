namespace NTKServer.ViewModels.Login
{
    public class LoginVm
    {
        public string LoginProvider { get; set; } = string.Empty;
        public string ProviderKey { get; set; } = string.Empty;
        public string? code { get; set; }
    }
}
