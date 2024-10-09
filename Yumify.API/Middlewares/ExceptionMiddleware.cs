using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Yumify.API.Helper;

namespace Yumify.API.Middlewares
{
    public class ExceptionMiddleware
    {
        public RequestDelegate RequestDelegate { get; set; }
        public IWebHostEnvironment Environment { get; set; }
        public ILogger<ExceptionMiddleware> Logger { get; set; }
        public ExceptionMiddleware(RequestDelegate _nextMiddleware, IWebHostEnvironment env, ILogger<ExceptionMiddleware> logger)
        {
            this.RequestDelegate = _nextMiddleware;
            this.Environment = env;
            this.Logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                //if i will handle any thing in the request

                await this.RequestDelegate.Invoke(context);

                //if i will handle any thing in the response
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                GeneralResponseServerError respon;

                if (Environment.IsDevelopment())
                {
                     respon= new GeneralResponseServerError(500,ex.Message,ex.StackTrace!);
                }
                else
                {
                     respon = new GeneralResponseServerError(500, ex.Message);
                }

                var json= JsonSerializer.Serialize(respon);

                await context.Response.WriteAsync(json);


            }
        }
    }
}
