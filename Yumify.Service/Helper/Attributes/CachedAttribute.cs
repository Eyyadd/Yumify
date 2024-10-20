using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumify.Core.IServices;

namespace Yumify.Service.Helper.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _CahceDuration;

        public CachedAttribute(int CahceDuration)
        {
            _CahceDuration = CahceDuration;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var reponseCahce = context.HttpContext.RequestServices.GetRequiredService<IResponseCahceService>();
            var cachKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var response = await reponseCahce.GetCacheResponse(cachKey);

            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType="application/json",
                    StatusCode=200
                };
                context.Result = result;
                return;
            }
            var ActionExcutedContext = await next.Invoke();
            if(ActionExcutedContext.Result is OkObjectResult okObjectResult)
            {
                await reponseCahce.CahceResponseAsync(cachKey, okObjectResult.Value, TimeSpan.FromSeconds(_CahceDuration));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append(request.Path);
            foreach (var (key,value) in request.Query.OrderBy(kv=>kv.Key))
            {
                KeyBuilder.Append($"|{key}-{value}");
            }

            return KeyBuilder.ToString();
        }
    }
}
