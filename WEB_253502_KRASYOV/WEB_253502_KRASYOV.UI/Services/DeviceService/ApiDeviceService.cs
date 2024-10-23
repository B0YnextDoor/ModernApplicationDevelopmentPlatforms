using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;
using WEB_253502_KRASYOV.UI.Services.Authentication;
using WEB_253502_KRASYOV.UI.Services.FileService;

namespace WEB_253502_KRASYOV.UI.Services.DeviceService
{
	public class ApiDeviceService : IDeviceService
	{
		private readonly HttpClient _httpClient;
		private readonly JsonSerializerOptions _serializerOptions;
		private readonly ILogger<ApiDeviceService> _logger;
        private readonly IFileService _fileService;
		private readonly ITokenAccessor _tokenAccessor;
        private readonly string _pageSize;
		private readonly string _uri;
		public ApiDeviceService(HttpClient httpClient, ITokenAccessor tokenAccessor,
			IConfiguration configuration, IFileService fileService, ILogger<ApiDeviceService> logger)
		{
			_httpClient = httpClient;
			_tokenAccessor = tokenAccessor;
			_fileService = fileService;
			_pageSize = configuration.GetSection("ItemsPerPage").Value;
			_serializerOptions = new JsonSerializerOptions()
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};
			_logger = logger;
			_uri = $"{_httpClient.BaseAddress.AbsoluteUri}Devices";
		}

		public async Task<ResponseData<Device>> CreateDeviceAsync(Device product, IFormFile? formFile)
		{
			product.Image = "images/noimage.jpg";

            if (formFile != null)
            {
                var imageUrl = await _fileService.SaveFileAsync(formFile);
                
                if (!string.IsNullOrEmpty(imageUrl))
                    product.Image = imageUrl;
            }

            var uri = new Uri(_uri);

			await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

			var response = await _httpClient.PostAsJsonAsync(uri, product, _serializerOptions);
			if (response.IsSuccessStatusCode)
			{
				try
				{
					return await response.Content
                        .ReadFromJsonAsync<ResponseData<Device>>(_serializerOptions);
				}
				catch (JsonException ex)
				{
					return ResponseData<Device>.Error(CatchJsonExceprion(ex));
				}
			}
			return ResponseData<Device>.Error(
				CatchServerError("Object not created", response.StatusCode));
		}

		public async Task DeleteDeviceAsync(int id)
		{
			var uri = new Uri($"{_uri}/{id}");
            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
            var response = await _httpClient.DeleteAsync(uri);
			if (!response.IsSuccessStatusCode) throw new Exception("Delete operation failed.");
			return;
		}

		public async Task<ResponseData<Device>> GetDeviceByIdAsync(int id)
		{
			var uri = new Uri($"{_uri}/{id}");
            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode) 
            {
                try
				{
					return await response.Content
						.ReadFromJsonAsync<ResponseData<Device>>(_serializerOptions);
				}
				catch (JsonException ex)
				{
					return ResponseData<Device>.Error(CatchJsonExceprion(ex));
				}
            }
			return ResponseData<Device>.Error(
				CatchServerError("Server data lost or not exist", response.StatusCode));
		}

		public async Task<ResponseData<ListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, 
			int pageNo = 1)
		{
			var urlString = new StringBuilder($"{_uri}/");
			
			if (categoryNormalizedName != null) urlString.Append($"{categoryNormalizedName}/");
			
			if (pageNo > 1) urlString.Append(QueryString.Create("pageNo", pageNo.ToString()));
			
			if (!_pageSize.Equals("3")) urlString.Append(QueryString.Create("pageSize", _pageSize));

			var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

			if (response.IsSuccessStatusCode)
			{
				try
				{
					return await response.Content
						.ReadFromJsonAsync<ResponseData<ListModel<Device>>>(_serializerOptions);
				}
				catch (JsonException ex)
				{
					return ResponseData<ListModel<Device>>.Error(CatchJsonExceprion(ex));
				}
			}
			return ResponseData<ListModel<Device>>.Error(
				CatchServerError("Server data lost or not exist", response.StatusCode));

		}

		public async Task UpdateDeviceAsync(int id, Device product, IFormFile? formFile)
		{
            if (formFile != null)
            {
                try
                {
					var fileName = product.Image.Split('/').Last();
                    await _fileService.DeleteFileAsync(fileName);
                }
                catch (Exception ex)
                {
                    throw;
                }

                var imageUrl = await _fileService.SaveFileAsync(formFile);

                if (!string.IsNullOrEmpty(imageUrl))
                    product.Image = imageUrl;
            }

            var uri = new Uri($"{_uri}/{id}");
            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
			_ = await _httpClient.PutAsJsonAsync(uri, product, _serializerOptions);
			return;
		}

		private string CatchJsonExceprion(JsonException ex)
		{
			var message = $"Error:\n{ex.Message}";
			_logger.LogError($"-----> {message}");
			return message;
		}

		private string CatchServerError(string error, HttpStatusCode code) {
			var message = $"{error}.\nError: {code}";
			_logger.LogError($"-----> {message}");
			return message;
		}
	}
}
