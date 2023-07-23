
namespace API.RateLimiting
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LimitRequests : Attribute
    {
        public int MaxRequests { get; set; }
        public int Window { get; set; }
    }
}