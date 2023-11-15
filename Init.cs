using NTKServer.Business;
using NTKServer.Libs;

namespace NTKServer
{
    public class Init
    {
        public static void Run()
        {
            AdminMenuBiz.BuildMenu();
            LanguageLib.InitErrorMsgCache();
            ConfigLib.Reset();
        }
    }
}
