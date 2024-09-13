using WEB_253502_KRASYOV.Domain.Entities;

namespace WEB_253502_KRASYOV.BlazorWasm.Services
{
    public interface IDataService
    {
        event Action DataLoaded;
        List<Category> Categories { get; set; }
        List<Device> Devices { get; set; }
        bool Success { get; set; }
        string ErrorMessage { get; set; }
        int TotalPages { get; set; }
        int CurrentPage { get; set; }
        Category SelectedCategory { get; set; }
        public Task GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
        public Task GetCategoryListAsync(int pageNo = 1);
    }
}
