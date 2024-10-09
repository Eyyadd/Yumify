using Yumify.Core.Entities;

namespace Yumify.API.Helper
{
    public class GeneralResponse
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public dynamic? Data { get; set; }

        public GeneralResponse(int statusCode, string? Message = null)
        {
            this.StatusCode = statusCode;
            this.Message = Message;
            Data = default;
        }
        public GeneralResponse(int statusCode, string Message, dynamic Data)
        {
            this.StatusCode = statusCode;
            this.Data = Data;
            this.Message = Message;
        }
        public string? chooseMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "You made a bad request",
                401 => "Sorry, you are Unauthorized",
                404 => "This not found",
                500 => "Server Side Error Try Again",
                200 => "Ok, Successfully",
                204 => "Returned Successfully, but there is no data",
                _ => " You made a big mistake"
            };
        }
    }
}
