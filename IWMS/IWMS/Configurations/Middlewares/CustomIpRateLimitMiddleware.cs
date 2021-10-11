using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IWMS.Configurations.Middlewares
{
    public class CustomIpRateLimitMiddleware : IpRateLimitMiddleware
    {
        private readonly IpRateLimitOptions _options;
        private readonly IIpPolicyStore _ipPolicyStore;

        public CustomIpRateLimitMiddleware(RequestDelegate next, IOptions<IpRateLimitOptions> options, IRateLimitCounterStore counterStore, IIpPolicyStore policyStore, IRateLimitConfiguration config, ILogger<IpRateLimitMiddleware> logger) : base(next, options, counterStore, policyStore, config, logger)
        {
            _options = options.Value;
            _ipPolicyStore = policyStore;
        }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            //var message = string.Format(_options.QuotaExceededResponse?.Content ??
            //    _options.QuotaExceededMessage ?? "Test：API calls quota exceeded! maximum admitted {0} per {1}.", rule.Limit, rule.Period, retryAfter);

            if (!_options.DisableRateLimitHeaders)
            {
                httpContext.Response.Headers["Retry-After"] = retryAfter;
            }

            httpContext.Response.StatusCode = _options.QuotaExceededResponse?.StatusCode ?? _options.HttpStatusCode;
            httpContext.Response.ContentType = _options.QuotaExceededResponse?.ContentType ?? "text/plain";

            return httpContext.Response.WriteAsync($"{{  \"errors\": {{ \"Code\": 429,\"Desc\": \"Every {rule.Period}second {rule.Limit} times,Please retry after {retryAfter} second!\"}} }}");
        }

    }
}
