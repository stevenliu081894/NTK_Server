using NLog;
namespace NTKServer.Libs
{
    public class LogLib
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Log(string message)
        {
            _logger.Info(message);
        }
    }
}
