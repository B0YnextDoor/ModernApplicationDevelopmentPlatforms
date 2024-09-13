using Microsoft.AspNetCore.Http.Extensions;
using Serilog;

namespace WEB_253502_KRASYOV.UI.Middleware
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;

		public LoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			await _next(httpContext);

			if (httpContext.Response.StatusCode < 200 || httpContext.Response.StatusCode >= 300)
			{
				Log.Error($"request {httpContext.Request.GetDisplayUrl()} returns {httpContext.Response.StatusCode}");
			}
		}
	}

	public static class LogginMiddlewareExtensions
	{
		public static IApplicationBuilder UseRequestLogginMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<LoggingMiddleware>();
		}
	}
}
