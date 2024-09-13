using Microsoft.AspNetCore.Http;
using WEB_253502_KRASYOV.UI.Services.Authentication;

namespace WEB_253502_KRASYOV.UI.Services.FileService
{
    public class ApiFileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenAccessor _tokenAccessor;
        public ApiFileService(HttpClient httpClient, ITokenAccessor tokenAccessor)
        {
            _httpClient = httpClient;
            _tokenAccessor = tokenAccessor;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var uri = $"{_httpClient.BaseAddress.AbsoluteUri}";
            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
            var response = await _httpClient.DeleteAsync($"{uri}?fileName={fileName}");
            if (!response.IsSuccessStatusCode) throw new Exception("Cannot delete file");
        }

        public async Task<string> SaveFileAsync(IFormFile formFile)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };
            
            var extension = Path.GetExtension(formFile.FileName);
            var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);
            
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(formFile.OpenReadStream());
            content.Add(streamContent, "file", newName);

            request.Content = content;
            await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return String.Empty;
        }
    }
}
