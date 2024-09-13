namespace WEB_253502_KRASYOV.UI.Extensions
{
	public static class HttpRequestExtensions
	{
		public static bool IsAjaxRequest(this HttpRequest request)
		{
			return request.Headers["X-Requested-With"] == "XMLHttpRequest";
		}
	}
}
