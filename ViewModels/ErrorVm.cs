namespace NTKServer.ViewModels;

public class ErrorVm
{
    public string Message { get; set; }
    public string Css { get; set; }
    public bool Show => !string.IsNullOrEmpty(Message);
}
