using System.Text;
using System.Text.Json;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.UI.Services.CategoryService
{
	public class ApiCategoryService : ICategoryService
	{
		private readonly HttpClient _httpClient;
		private readonly JsonSerializerOptions _serializerOptions;
		private readonly ILogger<ApiCategoryService> _logger;
		public ApiCategoryService(HttpClient httpClient, ILogger<ApiCategoryService> logger)
		{
			_httpClient = httpClient;
			_serializerOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};
			_logger = logger;
		}

		public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
		{

			var uriString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}Categories/");

			var response = await _httpClient.GetAsync(new Uri(uriString.ToString()));

			if (!response.IsSuccessStatusCode)
			{
				return ResponseData<List<Category>>.Error($"Error in fetching data: {response.StatusCode.ToString()}");
			}

			try
			{
				return await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions);
			}
			catch (Exception ex)
			{
				_logger.LogError($"-----> Error: {ex.Message}");
				return ResponseData<List<Category>>.Error($"Error: {ex.Message}");
			}
		}
	}
}
