namespace NTKServer.ViewModels.Menu
{
    public class MainMenuVm
    {
        public int Id { get; set; }
        public int Parent { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Active { get; set; }
        public string IconClass { get; set; } = string.Empty;
        public string Css { get; set; } = string.Empty;
        public List<MainMenuVm>? Child { get; set; }
    }
}
