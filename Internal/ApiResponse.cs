namespace NTKServer.Internal
{
    public class APIResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public object? data { get; set; }

        public static APIResponse Error(int status, string message)
        {
            APIResponse response = new APIResponse
            {
                status = status,
                message = message
            };
            return response;
        }

        public static APIResponse Ok(object? obj, string message = "")
        {
            APIResponse response = new APIResponse
            {
                status = 200,
                message = message,
                data = obj
            };
            return response;
        }
    }


    public class APIResponse<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public static APIResponse<T> Error(int status, string message)
        {
            APIResponse<T> response = new APIResponse<T>
            {
                status = status,
                message = message
            };
            return response;
        }

        public static APIResponse<T> Ok(T obj, string message = "")
        {
            APIResponse<T> response = new APIResponse<T>
            {
                status = 200,
                message = message,
                data = obj
            };
            return response;
        }
    }
}
