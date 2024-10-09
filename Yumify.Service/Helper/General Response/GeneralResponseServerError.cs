
namespace Yumify.API.Helper
{
    public class GeneralResponseServerError
    {
        public string? details { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        //public GeneralResponseServerError(int statusCode, string Message)
        //{
        //    this.StatusCode = statusCode;
        //    this.Message = Message;
        //    details = default;
        //}
        public GeneralResponseServerError(int statusCode, string Message, string? details = default)
        {
            this.StatusCode = statusCode;
            this.Message = Message;
            this.details = details;
        }
    }
}
