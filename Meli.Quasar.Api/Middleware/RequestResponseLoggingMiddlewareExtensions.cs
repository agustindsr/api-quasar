using Microsoft.AspNetCore.Builder;

namespace Meli.Quasar.Api.Middleware
{
    /// <summary>
    /// RequestResponseLoggingMiddlewareExtensions
    /// </summary>
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        /// <summary>
        /// UseRequestResponseLogging
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}