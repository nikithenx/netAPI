using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace API.RateLimiting
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;

        public RateLimitingMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var decorator = endpoint?.Metadata.GetMetadata<LimitRequests>();

            if (decorator is null)
            {
                await _next(context);
                return;
            }

            var key = GenerateClientKey(context);
            var clientStatistics = await GetClientStatisticsByKey(key);

            if (clientStatistics != null && 
                DateTime.UtcNow < clientStatistics.LastSuccessfulResponseTime.AddSeconds(decorator.Window) && 
                clientStatistics.NumberOfRequestsCompletedSuccessfully == decorator.MaxRequests)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                return;
            }

            await UpdateClientStatisticsStorage(key, decorator.MaxRequests);
            await _next(context);
        }

        private static string GenerateClientKey(HttpContext context) 
            => $"{context.Request.Path}_{context.Connection.RemoteIpAddress}";

        private async Task<T> GetObjectFromCache<T>(string key) 
        {
            var obj = await _cache.GetAsync(key);

            if (obj is null) 
                return await Task.FromResult(default(T));

            var deserialize = Encoding.UTF8.GetString(obj);
            return JsonSerializer.Deserialize<T>(deserialize);
        }

        private async Task<ClientStatistics> GetClientStatisticsByKey(string key) 
            => await GetObjectFromCache<ClientStatistics>(key);

        private async Task UpdateClientStatisticsStorage(string key, int maxRequests)
        {
            var statistics = await GetClientStatisticsByKey(key);

            if (statistics != null)
            {
                statistics.LastSuccessfulResponseTime = DateTime.UtcNow;

                if (statistics.NumberOfRequestsCompletedSuccessfully == maxRequests)
                {
                    statistics.NumberOfRequestsCompletedSuccessfully = 1;
                }
                else
                {
                    statistics.NumberOfRequestsCompletedSuccessfully++;
                }
            }
            else
            {
                statistics = new ClientStatistics
                {
                    LastSuccessfulResponseTime = DateTime.UtcNow,
                    NumberOfRequestsCompletedSuccessfully = 1
                };
            }

            var serialize = JsonSerializer.Serialize(statistics);
            await _cache.SetAsync(key, Encoding.UTF8.GetBytes(serialize));
        }
    }
}

