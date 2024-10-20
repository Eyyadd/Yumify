using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumify.Core.IServices
{
    public interface IResponseCahceService
    {
        Task CahceResponseAsync(string Key, object Response,TimeSpan CacheDuration);
        Task<string?> GetCacheResponse(string Key);
    }
}
