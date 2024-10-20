using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yumify.Core.IServices;

namespace Yumify.Service.Services
{
    public class ResponseCahceService : IResponseCahceService
    {
        private readonly IDatabase _Redis;

        public ResponseCahceService(IConnectionMultiplexer redis)
        {
            _Redis = redis.GetDatabase();
        }
        public async Task CahceResponseAsync(string Key, object Response, TimeSpan CacheDuration)
        {
            if (Response is not null)
            {
                var serialzationOptions= new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // the response should be in camel case to be handled with the front  
                var serializedResponse= JsonSerializer.Serialize(Response, serialzationOptions);
                await _Redis.StringSetAsync(Key, serializedResponse, CacheDuration);
            }
        }

        public async Task<string?> GetCacheResponse(string Key)
        {
          var response = await _Redis.StringGetAsync(Key);
            if(!response.IsNullOrEmpty)
            {
                return response;
            }
            return null;
        }
    }
}
