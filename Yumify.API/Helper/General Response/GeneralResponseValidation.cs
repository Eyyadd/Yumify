namespace Yumify.API.Helper
{
    public class GeneralResponseValidation
    {
        public int StatusCode { get; set; }
        public List<string>? Error { get; set; } = null;
        public string Message { get; set; }

        public GeneralResponseValidation(int statusCode,string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public GeneralResponseValidation(int statusCode, List<string> error, string message)
        {
            StatusCode = statusCode;
            Error = error;
            Message = message;
        }
    }
}
