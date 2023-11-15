using NTKServer.Libs;

namespace NTKServer.Internal
{
    public class AppException : Exception
    {
        private int _status;
        private string _messageKey;

        public AppException(string message)//指定错误消息
            : base(message)
        {
            _messageKey = message;
        }

        public AppException(int errorCode, string message) : base(message)
        {
            _status = errorCode;
            _messageKey = message;
        }

        public int GetStatus()
        {
            return _status;
        }

        public string GetMessage(string language)
        {
            return LanguageLib.GetErrorTranslate(language, _messageKey);
        }
    }
}
